using Music.controller;
using Music.util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Threading;

namespace Music.view
{
    /// <summary>
    /// Логика взаимодействия для CasualWindow.xaml
    /// </summary>
    public partial class CasualWindow : Window
    {
        private Controller controller;
        private MediaPlayer mediaPlayer;
        private SearchMusic searchMusic;
      
        private string songDuraction;
        public CasualWindow()
        {
            controller = new Controller();
            mediaPlayer = new MediaPlayer();
            searchMusic = new SearchMusic();
            InitializeComponent();
            App.LanguageChanged += LanguageChanged;
            ResizeMode = ResizeMode.NoResize;
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
                    new CasualWindow().Show();
                    Close();
                    mediaPlayer.Stop();
                }
            }

        }

        private void loadInSongGrid(object sender, RoutedEventArgs e)
        {
            SongDrid.ItemsSource = (List<Song>)controller.complete(TextCommand.GET_ALL_SONG, "");
        }
        private void playSelectedSong(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null)
            {

                //if (!isExistInFolder(song.Name))
                //{
                //    controller.complete(TextCommand.READ_GOOGLE_SONG_BY_ID, song.Id);
                //}

                string fileName = String.Format("{0}\\{1}", ExtraField.LISTEN_PATH, song.Name + ExtraField.CONTENT_TYPE.ToString());

                File.WriteAllBytes(fileName, song.MultimediaData);

                mediaPlayer.Open(new Uri(ExtraField.LISTEN_PATH + song.Name + ExtraField.CONTENT_TYPE));
               

                songDuraction = createNormalDuraction(song.Duraction);
                mediaPlayer.Play();
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();

            }

        }
        void timer_Tick(object sender, EventArgs e)
        {

            if (mediaPlayer.Source != null)
                lblStatus.Content = string.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), songDuraction);
            else
                lblStatus.Content = "No file selected...";
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

        private void songSound(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           mediaPlayer.Volume = SongSlider.Value/10;
        }

        private void searchParams(object sender, RoutedEventArgs e)
        {
            searchMusic.setCheckedOrNot(((CheckBox)sender).Name.ToString());
           
        }

        private void search(object sender, TextChangedEventArgs e)
        {
            if (!searchField.Text.Equals(""))
            {
                SongDrid.ItemsSource = searchMusic.getSongBySearchParams((List<Song>)SongDrid.ItemsSource, searchField.Text);
            }
            else
            {

                loadInSongGrid(sender, e);
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            new Welcome().Show();
            Close();
            mediaPlayer.Stop();
        }

        private void somt(object sender, MouseButtonEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null)
            {
                DescriptionField.Text = song.Description;
            }
        }
    }
}
