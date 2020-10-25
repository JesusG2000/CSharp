using Music.controller;
using Music.db;
using Music.dto;
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
    /// Логика взаимодействия для RegisterUserWindow.xaml
    /// </summary>
    public partial class RegisterUserWindow : Window
    {
        private DUser user;
        private Controller controller;
        private MediaPlayer mediaPlayer;
        private SearchMusic searchMusic;
       
        private string songDuraction;
        public RegisterUserWindow(DUser user)
        {
            controller = new Controller();
            mediaPlayer = new MediaPlayer();
            searchMusic = new SearchMusic();
            this.user = user;
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
                    new RegisterUserWindow(user).Show();
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
            mediaPlayer.Volume = SongSlider.Value / 10;
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
                showUsersComments(song);
            }
        }

        private void showUsersComments(Song song)
        {
            List<object> comments = new List<object>();
            List<Comment> songComments = (List<Comment>)controller.complete(TextCommand.GET_ALL_COMMENTS_BY_SONG_ID, song.Id);
            foreach (Comment c in songComments)
            {
                comments.Add(controller.complete(TextCommand.READ_USER_BY_ID, c.UserId));
                comments.Add(c);
            }
            commentList.ItemsSource = comments;
        }

        private void goToTheDiaologs(object sender, RoutedEventArgs e)
        {
            new DialogsWindow(user).Show();
            Close();
            mediaPlayer.Stop();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            new Welcome().Show();
            Close();
            mediaPlayer.Stop();
        }

        private void sendComment(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null && !commentField.Text.Trim().Equals(""))
            {

                Comment comment = new Comment();
                comment.SongId = song.Id;
                comment.UserId = user.Id;
                comment.Text = commentField.Text;
                comment.Date = DateTime.Now;
                commentField.Text = "";
                controller.complete(TextCommand.CREATE_COMMENT, comment);
                showUsersComments(song);
            }
        }

        private void loadInPlayListGrid(object sender, RoutedEventArgs e)
        {
            List<PlayList> playLists = (List<PlayList>)controller.complete(TextCommand.GET_ALL_PLAY_LISTS_BY_USER_ID, user.Id);
            playListDrid.ItemsSource = playLists;
            userPlayLists.ItemsSource = playLists;
        }

        private void createPlayList(object sender, RoutedEventArgs e)
        {
            new CreatePlayListWindow(user).Show();
            Close();
            mediaPlayer.Stop();
        }

        private void showAllSongs(object sender, RoutedEventArgs e)
        {
            loadInSongGrid(sender, e);
        }

        private void update(object sender, RoutedEventArgs e)
        {
            PlayList playList = (PlayList)playListDrid.SelectedItem;
            if (playList != null)
            {
                new CreatePlayListWindow(playList, user).Show();
                Close();
                mediaPlayer.Stop();
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            PlayList playList = (PlayList)playListDrid.SelectedItem;
            if (playList != null)
            {
                controller.complete(TextCommand.DELETE_PLAY_LIST, playList);
                loadInPlayListGrid(sender, e);
            }
        }

        private void showPlayListSongs(object sender, MouseButtonEventArgs e)
        {

            PlayList playList = (PlayList)playListDrid.SelectedItem;
            if (playList != null)
            {
                SongDrid.ItemsSource = (List<Song>)controller.complete(TextCommand.GET_ALL_SONGS_BY_PLAY_LIST_ID,playList.Id);
            }
        }

        private void addSongInPlayLists(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            if (song != null)
            {
                string text = userPlayLists.Text;
                if (!text.Equals(""))
                {
                    string[] playListsName = text.Split(',');
                    foreach (string s in playListsName)
                    {
                        PlayList playList = (PlayList)controller.complete(TextCommand.READ_PLAY_LIST_BY_NAME, s);
                        if (controller.complete(TextCommand.READ_PLAY_LIST_SONG_BY_SONG_AND_PLAY_LIST_IDS, new object[] { song.Id, playList.Id }) == null)
                        {
                            controller.complete(TextCommand.CREATE_PLAY_LIST_SONG, new PlayListSong(song.Id, playList.Id));
                        }
                    }
                }
            }
        }

        private void deleteSongFromPlayList(object sender, RoutedEventArgs e)
        {
            Song song = (Song)SongDrid.SelectedItem;
            PlayList playList = (PlayList)playListDrid.SelectedItem;
            if (song != null && playList!=null)
            {
                PlayListSong playListSong = (PlayListSong)controller.complete(TextCommand.READ_PLAY_LIST_SONG_BY_SONG_AND_PLAY_LIST_IDS, new object[] { song.Id, playList.Id });
                controller.complete(TextCommand.DELETE_PLAY_LIST_SONG, playListSong);
                showPlayListSongs(sender, null);
            }
        }
    }
}
