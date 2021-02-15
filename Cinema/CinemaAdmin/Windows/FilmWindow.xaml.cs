using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace CinemaAdmin.Windows
{
    public partial class FilmWindow : Window
    {
        #region Variables

        public Scripts.Engine.Status StatusData;

        public Scripts.Film Film;

        #endregion

        #region Constructors

        public FilmWindow()
        {
            InitializeComponent();

            Film = new Scripts.Film();
        }

        #endregion

        #region Windows Methods

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 60; i++)
            {
                if (i <= 24)
                {
                    ComboBoxHour.Items.Add($"{i}");
                }

                ComboBoxMinute.Items.Add($"{i}");
                ComboBoxSecond.Items.Add($"{i}");
            }

            if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                TextBoxName.Text = Film.Name;
                DatePickerStart.SelectedDate = Film.StartData;
                DatePickerEnd.SelectedDate = Film.EndData;
                ComboBoxHour.SelectedIndex = Film.Duration.Hours;
                ComboBoxMinute.SelectedIndex = Film.Duration.Minutes;
                ComboBoxSecond.SelectedIndex = Film.Duration.Seconds;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow(sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Scripts.Data.DataBaseManager.Status result = Scripts.Data.DataBaseManager.Status.NotFound;

            if (StatusData.Equals(Scripts.Engine.Status.Create))
            {
                result = Scripts.Data.DataBaseManager.AddFilm(new Scripts.Film
                (
                    id: 0,
                    filmName: TextBoxName.Text,
                    startData: DatePickerStart.SelectedDate.Value,
                    endData: DatePickerEnd.SelectedDate.Value,
                    duration: new TimeSpan(ComboBoxHour.SelectedIndex, ComboBoxMinute.SelectedIndex, ComboBoxSecond.SelectedIndex)
                ));
            }
            else if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                result = Scripts.Data.DataBaseManager.UpdateFilm(new Scripts.Film
                (
                    id: Film.Id,
                    filmName: TextBoxName.Text,
                    startData: DatePickerStart.SelectedDate.Value,
                    endData: DatePickerEnd.SelectedDate.Value,
                    duration: new TimeSpan(ComboBoxHour.SelectedIndex, ComboBoxMinute.SelectedIndex, ComboBoxSecond.SelectedIndex)
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonSave.IsEnabled = AccessToCreate();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private bool AccessToCreate()
        {
            return TextBoxName.Text.Length > 0 && DatePickerStart.SelectedDate.Value <= DatePickerEnd.SelectedDate.Value &&
                    ComboBoxHour.SelectedIndex > -1 && ComboBoxMinute.SelectedIndex > -1 && ComboBoxSecond.SelectedIndex > -1;
        }

        #endregion
    }
}
