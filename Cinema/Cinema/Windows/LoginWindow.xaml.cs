using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cinema.Scripts.DBase;

namespace Cinema
{
    public partial class LoginWindow : Window
    {
        #region Variables
        
        private readonly byte MinLengthLog = 4;
        
        private readonly byte MinLengthPas = 7;

        #endregion

        #region Constructors
        
        public LoginWindow()
        {
            InitializeComponent();
            /*
            if (DataBaseConnection.ConnectionOpen())
            {
                MessageBox.Show("Вдалося підключитися до бази!");
            }
            else
            {
                MessageBox.Show("Не вдалося підключитися до бази!");
            }*/
        }

        #endregion

        #region Windows Methods
        
        private void bttnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void LogWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow(sender, e);
        }
        
        private void bttnStateMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        private void LogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CheckConnectionDBase();
            LoadRememberMe();
        }

        private void LogWindow_Closed(object sender, EventArgs e)
        {

        }

        #region Window Login

        private void txtLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AccessToSymbol(e.Text);
        }
        
        private void txtPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AccessToSymbol(e.Text);
        }
        
        private void bttnSignIn_Click(object sender, RoutedEventArgs e)
        {
            bttnSignIn.IsEnabled = false;

            UsignIn();
            
            bttnSignIn.IsEnabled = true;
        }
        
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            AccessToUsignIn();
        }
        
        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            AccessToUsignIn();
        }

        private void lConnection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lConnection.IsEnabled = false;

            if (!CheckConnectionDBase())
            {
                MessageBox.Show(Languages.Language.ErrorCheckConnectionDB, "Date base connection", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                Scripts.LogFile.Log("Connecting to data base denied.", "Missing connection");
            }

            lConnection.IsEnabled = true;
        }

        #endregion

        #endregion

        #region Customers Methods

        private void AccessToUsignIn()
        {
            bttnSignIn.IsEnabled = txtLogin.Text.Length >= MinLengthLog && txtPassword.Password.Length >= MinLengthPas;
        }
        
        private void ClearAllTextBox()
        {
            txtLogin.Text = "";
            txtPassword.Password = "";
            checkRememberMe.IsChecked = false;
        }
        
        private void OpenMainWindow()
        {
            try
            {
                MainWindow main = new MainWindow();
                main.Show();

                if (!checkRememberMe.IsChecked.Value)
                {
                    ClearAllTextBox();
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                Scripts.LogFile.Log($"{ex}", "Error");
            }
        }
        
        private void UsignIn()
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            if (!CheckConnectionDBase())
            {
                MessageBox.Show(Languages.Language.SignInConnection, "", MessageBoxButton.OK, MessageBoxImage.Information);
                Scripts.LogFile.Log("Connecting to data base denied.", "Missing connection");
            }
            else
            {
                switch (DataBaseManager.SingIn(login, password))
                {
                    case DataBaseManager.Status.OK:
                        {
                            MessageBox.Show($"{Languages.Language.SignInSuccessful} {Account.UserName}.", "", MessageBoxButton.OK, MessageBoxImage.Information);

                            SaveRememberMe(checkRememberMe.IsChecked.Value);
                            OpenMainWindow();
                        }
                        break;
                    case DataBaseManager.Status.Unauthorized:
                        {
                            MessageBox.Show(Languages.Language.SignInUnauthorized, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case DataBaseManager.Status.Forbidden:
                        {
                            MessageBox.Show(Languages.Language.SignInForbidden, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case DataBaseManager.Status.NotFound:
                        {
                            MessageBox.Show(Languages.Language.SignInNotFound, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                }
            }
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton.Equals(MouseButtonState.Pressed))
            {
                this.DragMove();
            }
        }
        
        private bool AccessToSymbol(string Symbol)
        {
            return CodeGenerator.StringAccessSymbols.Contains(Symbol);
        }
        
        private void LoadRememberMe()
        {
            if (Properties.Settings.Default.isRememberMe)
            {
                txtLogin.Text = Properties.Settings.Default.UserLogin;
                txtPassword.Password = Properties.Settings.Default.UserPassword;
                checkRememberMe.IsChecked = Properties.Settings.Default.isRememberMe;
            }
        }
        
        private void SaveRememberMe(bool isRememberMe)
        {
            Properties.Settings.Default.UserLogin = isRememberMe ? txtLogin.Text : "";
            Properties.Settings.Default.UserPassword = isRememberMe ? txtPassword.Password : "";
            Properties.Settings.Default.isRememberMe = isRememberMe;

            Properties.Settings.Default.Save();
        }

        private bool CheckConnectionDBase()
        {
            bool isConnectionCorrect = DataBaseConnection.isConnectionCorrect;
            lConnection.Visibility = isConnectionCorrect ? Visibility.Hidden : Visibility.Visible;

            return isConnectionCorrect;
        }

        #endregion
    }
}