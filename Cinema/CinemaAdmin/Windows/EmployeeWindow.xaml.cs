using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaAdmin.Windows
{
    public partial class EmployeeWindow : Window
    {
        #region Variables

        public Scripts.Engine.Status StatusData;

        public Scripts.User User;

        private List<Scripts.Job> jobs;

        #endregion

        #region Constructors

        public EmployeeWindow()
        {
            InitializeComponent();

            User = new Scripts.User();
            jobs = new List<Scripts.Job>();
        }

        #endregion

        #region Windows Methods

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateComboBox();

            if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                int index = -1;

                foreach (var item in jobs)
                {
                    index++;

                    if (item.Name.Equals(User.Job.Name))
                    {
                        break;
                    }
                }

                TextBoxName.Text = User.Name;
                ComboBoxJobs.SelectedIndex = index;
                TextBoxLogin.Text = User.Login;
                CheckBoxIsActive.IsChecked = User.IsActive;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow(sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Scripts.Data.DataBaseManager.Status result = Scripts.Data.DataBaseManager.Status.NotFound;

            Scripts.Job job =
                (
                    from value
                        in jobs
                    where value.Name.Equals(ComboBoxJobs.Text)
                    select value
                ).FirstOrDefault();

            if (StatusData.Equals(Scripts.Engine.Status.Create))
            {
                result = Scripts.Data.DataBaseManager.Registration(new Scripts.User
                (
                    id: 0,
                    name: TextBoxName.Text,
                    job: job,
                    login: TextBoxLogin.Text,
                    password: Scripts.CodeGenerator.GetHashString(TextBoxPassword.Text).ToString(),
                    isActive: CheckBoxIsActive.IsChecked.Value
                ));
            }
            else if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                result = Scripts.Data.DataBaseManager.UpdateEmployee(new Scripts.User
                (
                    id: User.Id,
                    name: TextBoxName.Text,
                    job: job,
                    login: TextBoxLogin.Text,
                    password: TextBoxPassword.Text.Length > 0 ? Scripts.CodeGenerator.GetHashString(TextBoxPassword.Text).ToString() : User.Password,
                    isActive: CheckBoxIsActive.IsChecked.Value
                ));
            }

            switch (result)
            {
                case Scripts.Data.DataBaseManager.Status.OK:
                    {
                        MessageBox.Show($"Дані збережено успішно!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    break;
                case Scripts.Data.DataBaseManager.Status.LoginExists:
                    {
                        MessageBox.Show($"Даний логін використовується іншим користувачем.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
                case Scripts.Data.DataBaseManager.Status.Forbidden:
                    {
                        MessageBox.Show($"Не вдалося зберігти дані. Перевірте коректність введених даних.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    break;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonSave.IsEnabled = AccessToCreate();
        }

        private void ComboBoxJobs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonSave.IsEnabled = AccessToCreate();
        }

        #endregion

        #region Customer Methods

        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton.Equals(MouseButtonState.Pressed))
            {
                this.DragMove();
            }
        }

        private void UpdateComboBox()
        {
            Scripts.Engine.RefreshDataJobs(out jobs);

            ComboBoxJobs.ItemsSource =
            (
                from value
                    in jobs
                select value.Name
            );
        }

        private bool AccessToCreate()
        {
            bool IsUpdateStatus = StatusData.Equals(Scripts.Engine.Status.Update) ? (TextBoxPassword.Text.Length == 0 || TextBoxPassword.Text.Length >= Properties.Settings.Default.MinPasswordLength) : TextBoxPassword.Text.Length >= Properties.Settings.Default.MinPasswordLength;

            return TextBoxName.Text.Length > 0 && ComboBoxJobs.SelectedIndex > -1 && IsUpdateStatus && TextBoxLogin.Text.Length >= Properties.Settings.Default.MinLoginLength;
        }

        #endregion
    }
}
