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
    public partial class AdminWindow : Window
    {
        #region Variables

        private bool IsHideItems = true;

        private List<Scripts.Session> sessions;

        #endregion

        #region Constructors

        public AdminWindow()
        {
            InitializeComponent();
            Scripts.Account.DiactivateMethod += ChangeUser;

            sessions = new List<Scripts.Session>();
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
                UpdateSessionsInfo();
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
                    //this.PointToScreen(Mouse.GetPosition(this)).X;

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

        private void UpdateSessionsInfo()
        {
            sessions.Clear();

            Thread STAThread = new Thread(
                delegate()
                {
                    sessions = Scripts.Data.DataBaseManager.GetListSessions();
                });
            STAThread.Start();
            STAThread.Join();
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

        private void UpdateDataGridSessions()
        {
            string searchSession = TextBoxSearchSessions.Text;

            GridSessions.ItemsSource =
            (
                from value
                    in sessions
                where value.SessionData.ToShortDateString().Contains(searchSession) ||
                      value.SessionTime.ToShortTimeString().Contains(searchSession) ||
                      value.Film.Contains(searchSession) ||
                      value.Hall.Contains(searchSession) ||
                      value.Price.ToString().Contains(searchSession)
                group value by new { value.Film, value.Hall, value.SessionData, value.SessionTime }
                    into newGroup
                select newGroup.FirstOrDefault()
            );
        }

        private void TextBoxSearchSessions_TextChanged(object sender, TextChangedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchSessions, ref TextBoxSearchSessions);
            UpdateDataGridSessions();
        }

        private void TextBoxSearchSessions_GotFocus(object sender, RoutedEventArgs e)
        {
            GridSearchSessions.Visibility = Visibility.Hidden;
        }

        private void TextBoxSearchSessions_LostFocus(object sender, RoutedEventArgs e)
        {
            VisibilityGridSearchSessions(ref GridSearchSessions, ref TextBoxSearchSessions);
        }

        private void MenuItemRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateSessionsInfo();
            UpdateDataGridSessions();
        }
    }
}