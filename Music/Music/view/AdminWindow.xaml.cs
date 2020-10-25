using Music.dto;
using Music.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Music.util;
using System.Windows.Threading;
using System.Globalization;
using System.IO;
using Music.db;

namespace Music.view
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private DUser user;
        private Controller controller;
        private MediaPlayer mediaPlayer;
       
        private string songDuraction;
       


        public AdminWindow(DUser user)
        {
            this.user = user;
            controller = new Controller();
            mediaPlayer = new MediaPlayer();
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;


            menuLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }

        }

        private void loadInUserGrid(object sender, RoutedEventArgs e)
        {
            userGrid.ItemsSource = (List<DUser>)controller.complete(TextCommand.GET_ALL_REGISTERED_USERS, "");

        }
        private void songSound(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = SongSlider.Value / 10;
        }
        private void deleteSelectedFromUserGrid(object sender, RoutedEventArgs e)
        {
            DUser user = (DUser)userGrid.SelectedItem;

            if (user != null)
            {

                controller.complete(TextCommand.DELETE_USER, user);
                loadInUserGrid(sender, e);
            }
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            new Welcome().Show();
            Close();
            mediaPlayer.Stop();
        }

        private void dialogs(object sender, RoutedEventArgs e)
        {
            new DialogsWindow(user).Show();
            Close();
            mediaPlayer.Stop();
        }

        private void createSong(object sender, RoutedEventArgs e)
        {
            new CreateSongWindow(user).Show();
            Close();
            mediaPlayer.Stop();
        }



        private void deleteSong(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null)
            {
                controller.complete(TextCommand.DELETE_SONG, song);
                loadInSongGrid(sender, e);
                mediaPlayer.Stop();
                mediaPlayer.Close();
            }
        }

        private void loadInSongGrid(object sender, RoutedEventArgs e)
        {
            SongDrid.ItemsSource = (List<Song>)controller.complete(TextCommand.GET_ALL_SONG, "");
        }

        private void updateSong(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null)
            {
                new CreateSongWindow(user, song).Show();
                Close();
                mediaPlayer.Stop();
            }
        }

        private void playSelectedSong(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null)
            {

                mediaPlayer.Stop();
                mediaPlayer.Close();
                //if (!isExistInFolder(song.Name))
                //{
                //    controller.complete(TextCommand.READ_GOOGLE_SONG_BY_ID, song.Id);
                //}
                string fileName = String.Format("{0}\\{1}", ExtraField.LISTEN_PATH, song.Name + ExtraField.CONTENT_TYPE.ToString());

                File.WriteAllBytes(fileName, song.MultimediaData);

               

                mediaPlayer.Open(new Uri(ExtraField.LISTEN_PATH + song.Name + ExtraField.CONTENT_TYPE));

                // MessageBox.Show(LISTEN_PATH + song.Name + CONTENT_TYPE);

                songDuraction = createNormalDuraction(song.Duraction);
                mediaPlayer.Play();
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();

            }

        }

        private bool isExistInFolder(string name)
        {
            return File.Exists(ExtraField.LISTEN_PATH + name + ExtraField.CONTENT_TYPE);
        }

        private string createNormalDuraction(int duraction)
        {

            int min = duraction / 60;
            int sec = duraction - min * 60;
            return min + ":" + sec;
        }

        void timer_Tick(object sender, EventArgs e)
        {

            if (mediaPlayer.Source != null)
                lblStatus.Content = string.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), songDuraction);
            else
                lblStatus.Content = "No file selected...";
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                    new AdminWindow(user).Show();
                    Close();
                    mediaPlayer.Stop();
                }
            }

        }

        private void seeDescription(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null)
            {
                MessageBox.Show(song.Description);
            }
        }

        private void deleteAllUsers(object sender, RoutedEventArgs e)
        {
            List<DUser> regUsers = (List<DUser>)controller.complete(TextCommand.GET_ALL_REGISTERED_USERS, "");
            foreach(DUser u in regUsers)
            {
                controller.complete(TextCommand.DELETE_USER, u);
            }
            loadInUserGrid(sender, e);
        }
    }
}
