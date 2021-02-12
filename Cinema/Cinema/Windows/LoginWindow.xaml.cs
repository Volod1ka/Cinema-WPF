using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cinema
{
    public partial class LoginWindow : Window
    {
        #region Constructors
        
        public LoginWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Windows Methods
        
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void LoginWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow(sender, e);
        }
        
        private void ButtonStateMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Scripts.Data.DataBaseConnection.SetConnectingString(Properties.Resources.ConnectString);
            CheckConnectionDataBase();
            LoadRememberMe();
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {

        }

        #region Window Login

        private void TextBoxLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AccessToSymbol(e.Text);
        }
        
        private void TextBoxPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AccessToSymbol(e.Text);
        }
        
        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            ButtonSignIn.IsEnabled = false;

            UsignIn();

            ButtonSignIn.IsEnabled = true;
        }
        
        private void TextBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            AccessToUsignIn();
        }
        
        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            AccessToUsignIn();
        }

        private void LabelConnection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LabelConnection.IsEnabled = false;

            if (!CheckConnectionDataBase())
            {
                MessageBox.Show(Languages.Language.ErrorCheckConnectionDB, "Date base connection", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                Scripts.LogFile.Log("Connecting to data base denied.", "Missing connection");
            }

            LabelConnection.IsEnabled = true;
        }

        #endregion

        #endregion

        #region Customers Methods

        private void AccessToUsignIn()
        {
            ButtonSignIn.IsEnabled = TextBoxLogin.Text.Length >= Properties.Settings.Default.MinLoginLength && TextBoxPassword.Password.Length >= Properties.Settings.Default.MinPasswordLength;
        }
        
        private void ClearAllTextBox()
        {
            TextBoxLogin.Text = "";
            TextBoxPassword.Password = "";
            CheckBoxRememberMe.IsChecked = false;
        }
        
        private void OpenMainWindow()
        {
            try
            {
                Window main = new MainWindow();
                main.Show();

                if (!CheckBoxRememberMe.IsChecked.Value)
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
            string login = TextBoxLogin.Text;
            string password = TextBoxPassword.Password;

            if (!CheckConnectionDataBase())
            {
                MessageBox.Show(Languages.Language.SignInConnection, "", MessageBoxButton.OK, MessageBoxImage.Information);
                Scripts.LogFile.Log("Connecting to data base denied.", "Missing connection");
            }
            else
            {
                switch (Scripts.Data.DataBaseManager.SingIn(login, $"{Scripts.CodeGenerator.GetHashString(password)}"))
                {
                    case Scripts.Data.DataBaseManager.Status.OK:
                        {
                            MessageBox.Show($"{Languages.Language.SignInSuccessful} {Scripts.Account.UserName}.", "", MessageBoxButton.OK, MessageBoxImage.Information);

                            SaveRememberMe(CheckBoxRememberMe.IsChecked.Value);
                            OpenMainWindow();
                        }
                        break;
                    case Scripts.Data.DataBaseManager.Status.Unauthorized:
                        {
                            MessageBox.Show(Languages.Language.SignInUnauthorized, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case Scripts.Data.DataBaseManager.Status.Forbidden:
                        {
                            MessageBox.Show(Languages.Language.SignInForbidden, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case Scripts.Data.DataBaseManager.Status.NotFound:
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
            return Scripts.CodeGenerator.StringAccessSymbols.Contains(Symbol);
        }
        
        private void LoadRememberMe()
        {
            if (Properties.Settings.Default.isRememberMe)
            {
                TextBoxLogin.Text = Properties.Settings.Default.UserLogin;
                TextBoxPassword.Password = Properties.Settings.Default.UserPassword;
                CheckBoxRememberMe.IsChecked = Properties.Settings.Default.isRememberMe;
            }
        }
        
        private void SaveRememberMe(bool isRememberMe)
        {
            Properties.Settings.Default.UserLogin = isRememberMe ? TextBoxLogin.Text : "";
            Properties.Settings.Default.UserPassword = isRememberMe ? TextBoxPassword.Password : "";
            Properties.Settings.Default.isRememberMe = isRememberMe;

            Properties.Settings.Default.Save();
        }

        private bool CheckConnectionDataBase()
        {
            bool isConnectionCorrect = Scripts.Data.DataBaseConnection.isConnectionCorrect;
            LabelConnection.Visibility = isConnectionCorrect ? Visibility.Hidden : Visibility.Visible;

            return isConnectionCorrect;
        }

        #endregion
    }
}