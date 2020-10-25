using Music.controller;
using Music.db;
using Music.util;
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

namespace Music.view
{
    /// <summary>
    /// Логика взаимодействия для CreatePlayListWindow.xaml
    /// </summary>
    public partial class CreatePlayListWindow : Window
    {
        private DUser user;
        private Controller controller;
        private PlayListValidation playListValidation;
        private PlayList playList;
        public CreatePlayListWindow(DUser user)
        {
            this.user = user;
            controller = new Controller();
            playListValidation = new PlayListValidation();
            playList = new PlayList();
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            updateButton.IsEnabled = false;
        }
        public CreatePlayListWindow(PlayList playList ,DUser user)
        {
            this.user = user;
            controller = new Controller();
            playListValidation = new PlayListValidation();
            this.playList = playList;
            InitializeComponent();
            createButton.IsEnabled = false;
        }

        private void back(object sender, RoutedEventArgs e)
        {
            new RegisterUserWindow(user).Show();
            Close();
        }

        private void create(object sender, RoutedEventArgs e)
        {
            if (playListValidation.isAllFieldValidate() && ((PlayList)controller.complete(TextCommand.READ_PLAY_LIST_BY_NAME,playList.Name))==null)
            {
                playList.UserId = user.Id;
                controller.complete(TextCommand.CREATE_PLAY_LIST, playList);
                back(sender, e);
            }
            else
            {
                MessageBox.Show("Wrong fields or play list already exist");
            }
        }

        private void namevalidate(object sender, TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            playListValidation.playListNameValidation(text);
            playList.Name = text.Text;
        }

        private void descriptionValidation(object sender, TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            playListValidation.playListDescriptionValidation(text);
            playList.Description = text.Text;
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (playListValidation.isAllFieldValidate())
            {
                playList.UserId = user.Id;
                controller.complete(TextCommand.UPDATE_PLAY_LIST,new object[] { playList.Id, playList });
                back(sender, e);
            }
            else
            {
                MessageBox.Show("Wrong fields");
            }
        }
    }
}
