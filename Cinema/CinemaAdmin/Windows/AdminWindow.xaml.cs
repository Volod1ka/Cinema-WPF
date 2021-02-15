using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaAdmin
{
    public partial class AdminWindow : Window
    {
        #region Variables

        private bool IsHideItems = true;

        private List<Scripts.Session> sessions;
        private List<Scripts.Film> films;
        private List<Scripts.Hall> halls;
        private List<Scripts.User> employees;

        #endregion

        #region Constructors

        public AdminWindow()
        {
            InitializeComponent();
            Scripts.Account.DiactivateMethod += ChangeUser;

            sessions = new List<Scripts.Session>();
            films = new List<Scripts.Film>();
            halls = new List<Scripts.Hall>();
            employees = new List<Scripts.User>();
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

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuUserName.Header = $"{Scripts.Account.UserName}";//\xE77B

                Scripts.Engine.RefreshDataSessions(out sessions);
                Scripts.Engine.RefreshDataFilms(out films);
                Scripts.Engine.RefreshDataHalls(out halls);
                Scripts.Engine.RefreshDataEmployees(out employees);

                RefreshDataGridSessions();
                RefreshDataGridFilms();
                RefreshDataGridHalls();
                RefreshDataGridEmployees();
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
        
        #endregion

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ви бажаєте вийти з додатку?", "", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes))
            {
                Application.Current.Shutdown();
            }
        }

        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton.Equals(MouseButtonState.Pressed))
            {
                if (!this.WindowState.Equals(WindowState.Normal))
                {
                    double X = e.GetPosition(this).X;
                    WindowStateChange();
                    
                    this.Top = Math.Max(0, e.GetPosition(this).Y - GridTitleBar.Height / 2);
                    this.Left = (X * (SystemParameters.PrimaryScreenWidth - this.Width)) / SystemParameters.PrimaryScreenWidth;
                }

                this.DragMove();
            }
        }

        private void GridTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow(sender, e);
        }

        private void ButtonStateMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void ButtonStateChange_Click(object sender, RoutedEventArgs e)
        {
            WindowStateChange();
        }

        private async void TimerTick()
        {
            while (true)
            {
                LabelDateTime.Content = $"Дата: {DateTime.Now}";

                await System.Threading.Tasks.Task.Delay(1000);
            }
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

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            double width = IsHideItems ? 0 : 100;
        }

        private void VisibilityGridSearchSessions(ref Grid grid, ref TextBox textBox)
        {
            grid.Visibility = textBox.Text.Length > 0 || textBox.IsFocused ? Visibility.Hidden : Visibility.Visible;
        }

        private void TextBoxSearchSessions_TextChanged(object sender, TextChangedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchSessions, ref TextBoxSearchSessions);
            RefreshDataGridSessions();
        }

        private void TextBoxSearchSessions_GotFocus(object sender, RoutedEventArgs e)
        {
            GridSearchSessions.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearchSessions_LostFocus(object sender, RoutedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchSessions, ref TextBoxSearchSessions);
        }

        private void MenuItemRefreshSessions_Click(object sender, RoutedEventArgs e)
        {
            Scripts.Engine.RefreshDataSessions(out sessions);
            RefreshDataGridSessions();
        }

        private void MenuItemRefreshFilms_Click(object sender, RoutedEventArgs e)
        {
            Scripts.Engine.RefreshDataFilms(out films);
            RefreshDataGridFilms();
        }

        private void MenuItemRefreshHalls_Click(object sender, RoutedEventArgs e)
        {
            Scripts.Engine.RefreshDataHalls(out halls);
            RefreshDataGridHalls();
        }

        private void MenuItemRefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            Scripts.Engine.RefreshDataEmployees(out employees);
            RefreshDataGridEmployees();
        }

        private void MenuItemAddEmployees_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow(Scripts.Engine.Status.Create, sender, e);
        }

        private void MenuItemUpdateEmployees_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow(Scripts.Engine.Status.Update, sender, e);
        }

        private void EmployeeWindow(Scripts.Engine.Status status, object sender, RoutedEventArgs e)
        {
            Windows.EmployeeWindow window = new Windows.EmployeeWindow();
            window.StatusData = status;

            if (status.Equals(Scripts.Engine.Status.Update))
            {
                window.User = (Scripts.User)GridEmployees.SelectedItem;
            }

            window.ShowDialog();
            MenuItemRefreshEmployees_Click(sender, e);
        }

        private void RefreshDataGridSessions()
        {
            string searchSession = TextBoxSearchSessions.Text;

            GridSessions.ItemsSource = Scripts.Engine.RefreshDataGrid(() =>
            {
                return
                (
                    from value
                        in sessions
                    where value.SessionData.ToShortDateString().Contains(searchSession) ||
                          value.SessionTime.ToString().Contains(searchSession) ||
                          value.Film.Name.Contains(searchSession) ||
                          value.Hall.Name.Contains(searchSession) ||
                          value.Price.ToString().Contains(searchSession)
                    select value
                );
            });
        }

        private void RefreshDataGridFilms()
        {
            string searchFilms = TextBoxSearchFilms.Text;

            GridFilms.ItemsSource = Scripts.Engine.RefreshDataGrid(() =>
            {
                return
                (
                    from value
                        in films
                    where value.Name.Contains(searchFilms) ||
                          value.Duration.ToString().Contains(searchFilms) ||
                          value.StartData.ToString().Contains(searchFilms) ||
                          value.EndData.ToString().Contains(searchFilms)
                    select value
                );
            });
        }

        private void RefreshDataGridHalls()
        {
            string searchHalls = TextBoxSearchHalls.Text;

            GridHalls.ItemsSource = Scripts.Engine.RefreshDataGrid(() =>
            {
                return 
                (
                    from value
                        in halls
                    where value.Name.Contains(searchHalls) ||
                          value.Rows.ToString().Contains(searchHalls) ||
                          value.Seats.ToString().Contains(searchHalls)
                    select value
                );
            });
        }

        private void RefreshDataGridEmployees()
        {
            string searchEmployees = TextBoxSearchEmployees.Text;

            GridEmployees.ItemsSource = Scripts.Engine.RefreshDataGrid(() =>
            {
                return
                (
                    from value
                        in employees
                    where value.Name.Contains(searchEmployees) ||
                          value.Job.Name.Contains(searchEmployees) ||
                          value.Login.Contains(searchEmployees)
                    select value
                );
            });
        }

        private void TextBoxSearchFilms_TextChanged(object sender, TextChangedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchFilms, ref TextBoxSearchFilms);
            RefreshDataGridFilms();
        }

        private void TextBoxSearchFilms_GotFocus(object sender, RoutedEventArgs e)
        {
            GridSearchFilms.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearchFilms_LostFocus(object sender, RoutedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchFilms, ref TextBoxSearchFilms);
        }

        private void TextBoxSearchHalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchHalls, ref TextBoxSearchHalls);
            RefreshDataGridHalls();
        }

        private void TextBoxSearchHalls_GotFocus(object sender, RoutedEventArgs e)
        {
            GridSearchHalls.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearchHalls_LostFocus(object sender, RoutedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchHalls, ref TextBoxSearchHalls);
        }

        private void TextBoxSearchEmployees_TextChanged(object sender, TextChangedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchEmployees, ref TextBoxSearchEmployees);
            RefreshDataGridEmployees();
        }

        private void TextBoxSearchEmployees_GotFocus(object sender, RoutedEventArgs e)
        {
            GridSearchEmployees.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearchEmployees_LostFocus(object sender, RoutedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchEmployees, ref TextBoxSearchEmployees);
        }

        private void GridEmployees_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            MenuItemUpdateEmployees.IsEnabled = GridEmployees.SelectedIndex > -1;
        }

        private void HallWindow(Scripts.Engine.Status status, object sender, RoutedEventArgs e)
        {
            Windows.HallWindow window = new Windows.HallWindow();
            window.StatusData = status;

            if (status.Equals(Scripts.Engine.Status.Update))
            {
                window.Hall = (Scripts.Hall)GridHalls.SelectedItem;
            }

            window.ShowDialog();
            MenuItemRefreshHalls_Click(sender, e);
        }

        private void MenuItemUpdateHalls_Click(object sender, RoutedEventArgs e)
        {
            HallWindow(Scripts.Engine.Status.Update, sender, e);
        }

        private void MenuItemAddHalls_Click(object sender, RoutedEventArgs e)
        {
            HallWindow(Scripts.Engine.Status.Create, sender, e);
        }

        private void GridHalls_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            MenuItemUpdateHalls.IsEnabled = GridHalls.SelectedIndex > -1;
        }

        private void FilmWindow(Scripts.Engine.Status status, object sender, RoutedEventArgs e)
        {
            Windows.FilmWindow window = new Windows.FilmWindow();
            window.StatusData = status;

            if (status.Equals(Scripts.Engine.Status.Update))
            {
                window.Film = (Scripts.Film)GridFilms.SelectedItem;
            }

            window.ShowDialog();
            MenuItemRefreshFilms_Click(sender, e);
        }

        private void MenuItemAddFilms_Click(object sender, RoutedEventArgs e)
        {
            FilmWindow(Scripts.Engine.Status.Create, sender, e);
        }

        private void MenuItemUpdateFilms_Click(object sender, RoutedEventArgs e)
        {
            FilmWindow(Scripts.Engine.Status.Update, sender, e);
        }

        private void GridFilms_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            MenuItemUpdateFilms.IsEnabled = GridFilms.SelectedIndex > -1;
        }

        private void SessionWindow(Scripts.Engine.Status status, object sender, RoutedEventArgs e)
        {
            Windows.SessionWindow window = new Windows.SessionWindow();
            window.StatusData = status;
            window.Films = films;
            window.Halls = halls;

            if (status.Equals(Scripts.Engine.Status.Update))
            {
                window.Session = (Scripts.Session)GridSessions.SelectedItem;
            }

            window.ShowDialog();
            MenuItemRefreshSessions_Click(sender, e);
        }

        private void MenuItemAddSessions_Click(object sender, RoutedEventArgs e)
        {
            SessionWindow(Scripts.Engine.Status.Create, sender, e);
        }

        private void MenuItemUpdateSessions_Click(object sender, RoutedEventArgs e)
        {
            SessionWindow(Scripts.Engine.Status.Update, sender, e);
        }

        private void GridSessions_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            MenuItemUpdateSessions.IsEnabled = GridSessions.SelectedIndex > -1;
        }
    }
}