using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaAdmin.Windows
{
    public partial class HallWindow : Window
    {
        #region Variables

        public Scripts.Engine.Status StatusData;

        public Scripts.Hall Hall;

        #endregion

        #region Constructors

        public HallWindow()
        {
            InitializeComponent();

            Hall = new Scripts.Hall();
        }

        #endregion

        #region Windows Methods

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                TextBoxName.Text = Hall.Name;
                TextBoxRows.Text = $"{Hall.Rows}";
                TextBoxRows.IsEnabled = false;
                TextBoxSeats.Text = $"{Hall.Seats}";
                TextBoxSeats.IsEnabled = false;
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
                result = Scripts.Data.DataBaseManager.AddHallAndSeats(new Scripts.Hall
                (
                    id: 0,
                    name: TextBoxName.Text,
                    rows: uint.Parse(TextBoxRows.Text),
                    seats: uint.Parse(TextBoxSeats.Text)
                ));
            }
            else if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                result = Scripts.Data.DataBaseManager.UpdateHall(new Scripts.Hall
                (
                    id: Hall.Id,
                    name: TextBoxName.Text,
                    rows: uint.Parse(TextBoxRows.Text),
                    seats: uint.Parse(TextBoxSeats.Text)
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
            bool access = false;

            try
            {
                access = TextBoxName.Text.Length > 0 && uint.Parse(TextBoxRows.Text) > 0 && uint.Parse(TextBoxSeats.Text) > 0;
            }
            catch
            {
                access = false;
            }

            return access;
        }

        #endregion
    }
}
