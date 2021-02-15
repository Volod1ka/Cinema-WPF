using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaAdmin.Windows
{
    public partial class SessionWindow : Window
    {
        #region Variables

        public Scripts.Engine.Status StatusData;

        public List<Scripts.Hall> Halls;
        
        public List<Scripts.Film> Films;

        public Scripts.Session Session;

        private List<DateTime> DateSession;

        #endregion

        #region Constructors

        public SessionWindow()
        {
            InitializeComponent();

            Films = new List<Scripts.Film>();
            Halls = new List<Scripts.Hall>();
            Session = new Scripts.Session();
            DateSession = new List<DateTime>();
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

            UpdateComboBox();

            if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                ComboBoxHall.Text = Session.Hall.Name;
                ComboBoxHall.IsEnabled = false;
                ComboBoxFilm.Text = Session.Film.Name;
                ComboBoxDay.Text = Session.SessionData.ToShortDateString();
                ComboBoxHour.SelectedIndex = Session.SessionTime.Hours;
                ComboBoxMinute.SelectedIndex = Session.SessionTime.Minutes;
                ComboBoxSecond.SelectedIndex = Session.SessionTime.Seconds;
                TextBoxPrice.Text = $"{Session.Price}";
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
                result = Scripts.Data.DataBaseManager.AddSessionAndTickets(new Scripts.Session
                (
                    id: 0,
                    film: Films[ComboBoxFilm.SelectedIndex],
                    sessionData: DateSession[ComboBoxDay.SelectedIndex],
                    sessionTime: new TimeSpan(ComboBoxHour.SelectedIndex, ComboBoxMinute.SelectedIndex, ComboBoxSecond.SelectedIndex),
                    hall: Halls[ComboBoxHall.SelectedIndex],
                    price: float.Parse(TextBoxPrice.Text)
                ));
            }
            else if (StatusData.Equals(Scripts.Engine.Status.Update))
            {
                result = Scripts.Data.DataBaseManager.UpdateSession(new Scripts.Session
                (
                    id: Session.Id,
                    film: Films[ComboBoxFilm.SelectedIndex],
                    sessionData: DateSession[ComboBoxDay.SelectedIndex],
                    sessionTime: new TimeSpan(ComboBoxHour.SelectedIndex, ComboBoxMinute.SelectedIndex, ComboBoxSecond.SelectedIndex),
                    hall: Session.Hall,
                    price: float.Parse(TextBoxPrice.Text)
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonSave.IsEnabled = AccessToCreate();
        }

        private void ComboBoxFilm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDateSessionList();
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
            foreach (var item in Films)
            {
                ComboBoxFilm.Items.Add(item.Name);
            }

            foreach (var item in Halls)
            {
                ComboBoxHall.Items.Add(item.Name);
            }
        }

        private void LoadDateSessionList()
        {
            DateSession.Clear();

            for (DateTime i = Films[ComboBoxFilm.SelectedIndex].StartData; i <= Films[ComboBoxFilm.SelectedIndex].EndData; i = i.AddDays(1))
            {
                DateSession.Add(i);
                ComboBoxDay.Items.Add(i.ToShortDateString());
            }
        }

        private bool AccessToCreate()
        {
            bool access = false;

            try
            {
                access = ComboBoxHall.SelectedIndex > -1 && ComboBoxFilm.SelectedIndex > -1 && ComboBoxDay.SelectedIndex > -1 &&
                ComboBoxHour.SelectedIndex > -1 && ComboBoxMinute.SelectedIndex > -1 && ComboBoxSecond.SelectedIndex > -1 &&
                float.Parse(TextBoxPrice.Text) > 0;
            }
            catch (Exception)
            {
                access = false;
            }

            return access;
        }

        #endregion
    }
}
