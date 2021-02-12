using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Cinema
{
    public partial class MainWindow : Window
    {
        #region Variables
        
        private bool GridTicketsLeftIsVisible = true;

        private List<Scripts.Ticket> tickets;

        #endregion

        #region Constructors
        
        public MainWindow()
        {
            InitializeComponent();
            Scripts.Account.DiactivateMethod += ChangeUser;

            tickets = new List<Scripts.Ticket>();
        }

        #endregion

        #region Windows Methods

        #region Menu Methods

        private void MenuChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ви бажаєте змінити користувача?", "", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
            {
                Scripts.Account.DiactivateAccount(message: false);
            }
        }

        private void MenuItemSell_Click(object sender, RoutedEventArgs e)
        {
            if (SellTicket(isSell: true))
            {
                int index = GridTickets.SelectedIndex;

                TicketsUpdate(index: index, isPaid: true);
            }
        }

        private void MenuItemBack_Click(object sender, RoutedEventArgs e)
        {
            if (SellTicket(isSell: false))
            {
                int index = GridTickets.SelectedIndex;

                TicketsUpdate(index: index);
            }
        }

        private void MenuItemToBook_Click(object sender, RoutedEventArgs e)
        {
            int index = GridTickets.SelectedIndex;

            TicketsUpdate(index: index, isToBook: true);
        }

        private void MenuItemToBookCancel_Click(object sender, RoutedEventArgs e)
        {
            int index = GridTickets.SelectedIndex;

            TicketsUpdate(index: index);
        }

        private void MenuItemToBookCancelAll_Click(object sender, RoutedEventArgs e)
        {
            for (int index = 0; index < GridTickets.Items.Count; index++)
            {
                var cellInfo = GridTickets.Items[index] as Scripts.Ticket;

                if (!cellInfo.IsPaid && cellInfo.IsToBook)
                {
                    TicketsUpdate(index: index);
                }
            }
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuUserName.Header = $"{Scripts.Account.UserName}";//\xE77B

                Scripts.Engine.RefreshDataTickets(out tickets);
                UpdateDataGridSessions();
            }
            catch (Exception ex)
            {
                Scripts.LogFile.Log($"{ex.Message}", "Error");
            }

            TimerTick();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Scripts.Account.DiactivateMethod -= ChangeUser;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Normal:
                    {
                        ButtonStateChange.Content = "\xE740";
                    }
                    break;
                case WindowState.Maximized:
                    {
                        ButtonStateChange.Content = "\xE73F";
                    }
                    break;
            }
        }

        private void ButtonStateMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonStateChange_Click(object sender, RoutedEventArgs e)
        {
            WindowStateChange();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ви бажаєте вийти з додатку?", "", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
            {
                Application.Current.Shutdown();
            }
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            double newWidth;
            Storyboard storyboard = new Storyboard();

            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };

            DoubleAnimation animation = new DoubleAnimation();

            if (!GridTicketsLeftIsVisible)
            {
                newWidth = this.MinWidth * 0.64f;//0.25f/*190*/;
            }
            else
            {
                newWidth = 0;
            }

            animation.Completed += AnimationCompleted;
            animation.From = GridLeft.ActualWidth;
            animation.To = newWidth;
            animation.EasingFunction = ease;
            animation.Duration = TimeSpan.FromSeconds(0.4d);
            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, GridLeft);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));

            storyboard.Begin();

            GridTicketsLeftIsVisible = !GridTicketsLeftIsVisible;
        }

        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow(sender, e);
        }

        private void MenuItemRefresh_Click(object sender, RoutedEventArgs e)
        {
            Scripts.Engine.RefreshDataTickets(out tickets);
            UpdateDataGridSessions();

            InsertInfoTicketsCount(SellTickets: "", ToBookTickets: "", LastTickets: "");
        }

        private void GridTickets_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            int index = GridTickets.SelectedIndex;
            bool access = index > -1;

            Scripts.Ticket cellInfo = access ? (Scripts.Ticket)GridTickets.SelectedCells[index].Item : new Scripts.Ticket();

            MenuItemSell.IsEnabled = access ? !cellInfo.IsPaid : false;
            MenuItemBack.IsEnabled = access ? cellInfo.IsPaid : false;
            MenuItemToBook.IsEnabled = access ? !cellInfo.IsPaid && !cellInfo.IsToBook : false;
            MenuItemToBookCancel.IsEnabled = access ? !cellInfo.IsPaid && cellInfo.IsToBook : false;
            MenuItemToBookCancelAll.IsEnabled = access;
        }

        private void GridTickets_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            UpdateDataGridTickets();
        }

        private void GridSessions_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            UpdateDataGridTickets();
        }

        private void ComboBoxFilms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateDataGridSessions(isUpdateComboBox: false);
        }

        private void TextBoxSearchSessions_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchSessions, ref TextBoxSearchSessions);
            UpdateDataGridSessions(isUpdateComboBox: false);
        }

        private void TextBoxSearchTickets_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchTickets, ref TextBoxSearchTickets);
            UpdateDataGridTickets();
        }

        private void TextBoxSearchSessions_GotFocus(object sender, RoutedEventArgs e)
        {
            GridSearchSessions.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearchSessions_LostFocus(object sender, RoutedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchSessions, ref TextBoxSearchSessions);
        }

        private void TextBoxSearchTickets_GotFocus(object sender, RoutedEventArgs e)
        {
            GridSearchTickets.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearchTickets_LostFocus(object sender, RoutedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchTickets, ref TextBoxSearchTickets);
        }

        #endregion

        #region Customers Methods

        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton.Equals(MouseButtonState.Pressed))
            {
                if (!this.WindowState.Equals(WindowState.Normal))
                {
                    double X = e.GetPosition(this).X;
                    //this.PointToScreen(Mouse.GetPosition(this)).X;

                    WindowStateChange();
                    
                    this.Top = Math.Max(0, e.GetPosition(this).Y - GridTitleBar.Height / 2);
                    this.Left = (X * (SystemParameters.PrimaryScreenWidth - this.Width)) / SystemParameters.PrimaryScreenWidth;
                }

                this.DragMove();
            }
        }

        private void WindowStateChange()
        {
            switch (this.WindowState)
            {
                case WindowState.Normal:
                    {
                        this.WindowState = WindowState.Maximized;
                    }
                    break;
                case WindowState.Maximized:
                    {
                        this.WindowState = WindowState.Normal;
                    }
                    break;
            }
        }
                
        private void AnimationCompleted(object sender, EventArgs e)
        {
            GridLeft.IsEnabled = true;
        } 

        private async void TimerTick()
        {
            while (true)
            {
                LabelDateTime.Content = $"{Languages.Language.LabelDateTime}: {DateTime.Now}";

                await System.Threading.Tasks.Task.Delay(1000);
            }
        }

        private void UpdateComboBox()
        {
            DateTime dateTimeNow = DateTime.Now;
            ComboBoxFilms.ItemsSource =            
            (
                from value
                    in tickets
                where value.Session.SessionData.AddHours(value.Session.SessionTime.Hour).AddMinutes(value.Session.SessionTime.Minute + 15) >= dateTimeNow
                group value by new { value.Session.Film.Name }
                    into newGroup
                select newGroup.FirstOrDefault().Session.Film.Name
            );

            ComboBoxHalls.ItemsSource =
            (
                from value
                    in tickets
                where value.Session.SessionData.AddHours(value.Session.SessionTime.Hour).AddMinutes(value.Session.SessionTime.Minute + 15) >= dateTimeNow
                group value by new { value.Session.Hall.Name }
                    into newGroup
                select newGroup.FirstOrDefault().Session.Hall.Name
            );
        }

        private void UpdateDataGridTickets()
        {
            int index = GridSessions.SelectedIndex;

            if (index > -1)
            {
                string searchTicket = TextBoxSearchTickets.Text;
                var cellInfo = GridSessions.SelectedCells[index].Item as Scripts.Ticket;
                var tableTickets = Scripts.Engine.RefreshDataGrid(() =>
                {
                    return
                    (
                        from value
                            in tickets
                        where value.Session.Film.Name == cellInfo.Session.Film.Name && value.Session.SessionData == cellInfo.Session.SessionData && value.Session.SessionTime == cellInfo.Session.SessionTime
                        && (value.Row.ToString().Contains(searchTicket) || value.Seat.ToString().Contains(searchTicket))
                        select value
                    );
                });

                GridTickets.ItemsSource = tableTickets;

                int sellTickets =
                (
                    from value
                        in tableTickets
                    where value.IsPaid == true
                    select value
                ).Count();

                int toBookTickets =
                (
                    from value
                        in tableTickets
                    where value.IsToBook == true
                    select value
                ).Count();

                InsertInfoTicketsCount(SellTickets: $"{Math.Max(sellTickets, 0)}", ToBookTickets: $"{Math.Max(toBookTickets, 0)}", LastTickets: $"{Math.Max(tableTickets.Count() - sellTickets - toBookTickets, 0)}");
            }
            else
            {
                GridTickets.ItemsSource = null;

                InsertInfoTicketsCount(SellTickets: "", ToBookTickets: "", LastTickets: "");
            }
        }

        private void UpdateDataGridSessions(bool isUpdateComboBox = true)
        {
            if (isUpdateComboBox)
            {
                UpdateComboBox();
            }

            string film = ComboBoxFilms.SelectedIndex < 0 ? "" : ComboBoxFilms.SelectedItem.ToString();
            string hall = ComboBoxHalls.SelectedIndex < 0 ? "" : ComboBoxHalls.SelectedItem.ToString();
            string searchSession = TextBoxSearchSessions.Text;

            GridSessions.ItemsSource = Scripts.Engine.RefreshDataGrid(() =>
            {
                return
                (
                   from value
                       in tickets
                   where value.Session.SessionData.AddHours(value.Session.SessionTime.Hour).AddMinutes(value.Session.SessionTime.Minute + 15) >= DateTime.Now &&
                      (value.Session.SessionData.ToShortDateString().Contains(searchSession) ||
                      value.Session.SessionTime.ToShortTimeString().Contains(searchSession) ||
                      value.Session.Price.ToString().Contains(searchSession))
                   group value by new { Film = value.Session.Film.Name, Hall = value.Session.Hall.Name, value.Session.SessionData, value.Session.SessionTime }
                       into newGroup
                   where newGroup.Key.Film == film && newGroup.Key.Hall == hall
                   select newGroup.FirstOrDefault()
               );
            });
        }

        private void ChangeUser(bool message = true)
        {
            if (message)
            {
                MessageBox.Show(Languages.Language.SignInUnauthorized, "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Application.Current.Windows[0].Show();
            Scripts.Account.Clear();
            this.Close();
        }

        private bool SellTicket(bool isSell)
        {
            bool result = false;
            int index = GridTickets.SelectedIndex;

            if (index > -1)
            {
                var cellInfo = GridTickets.SelectedCells[index].Item as Scripts.Ticket;

                if (cellInfo.Session.SessionData.AddHours(cellInfo.Session.SessionTime.Hour).AddMinutes(cellInfo.Session.SessionTime.Minute + 15) >= DateTime.Now)
                {
                    result = isSell ? Scripts.Data.DataBaseManager.SellTicket(ticket: cellInfo.Id) : Scripts.Data.DataBaseManager.BackTicket(ticket: cellInfo.Id);
                }
                
                if (!result)
                {
                    MessageBox.Show("Не вдалося виконати операцію. Обновіть дані та повторіть спробу.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            return result;
        }

        private void TicketsUpdate(int index = -1, bool isPaid = false, bool isToBook = false)
        {
            if (index > -1 && index < GridTickets.Items.Count)
            {
                var cellInfo = GridTickets.Items[index] as Scripts.Ticket;

                if (Scripts.Data.DataBaseManager.UpdateTicket(ticket: cellInfo.Id, isPaid: isPaid, isToBook: isToBook))
                {
                    cellInfo.Input
                        (
                            id: cellInfo.Id,
                            session: cellInfo.Session,
                            dataCreate: cellInfo.DataCreate,
                            row: cellInfo.Row,
                            seat: cellInfo.Seat,
                            isPaid: isPaid,
                            isToBook: isToBook
                        );

                    GridTickets.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Не вдалося виконати операцію. Обновіть дані та повторіть спробу.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void InsertInfoTicketsCount(string SellTickets, string ToBookTickets, string LastTickets)
        {
            TextBoxSellTickets.Text = SellTickets;
            TextBoxToBookTickets.Text = ToBookTickets;
            TextBoxLastTickets.Text = LastTickets;
        }

        private void VisibilityGridSearchSessions(ref Grid grid, ref TextBox textBox)
        {
            grid.Visibility = textBox.Text.Length > 0 || textBox.IsFocused ? Visibility.Hidden : Visibility.Visible;
        }

        #endregion
    }
}