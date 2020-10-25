using Music.dto;
using Music.dto.dto;
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
using System.Threading;
using Music.db;

namespace Music.view
{
    /// <summary>
    /// Логика взаимодействия для DialogsWindow.xaml
    /// </summary>
    public partial class DialogsWindow : Window
    {
        private DUser user;
        private Dialog selectedDialog;
        private Controller controller;
        private bool endDialogs;

        public DialogsWindow(DUser user)
        {
            this.user = user;
            controller = new Controller();
            endDialogs = false;
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void loadInUserDialogGrid(object sender, RoutedEventArgs e)
        {
            List<Dialog> dialogs = (List<Dialog>)controller.complete(TextCommand.CREATE_USER_DIALOGS, user.Id);
            registeredUsers.ItemsSource = (List<DUser>)controller.complete(TextCommand.GET_ALL_REGISTERED_USERS, "");
            if (dialogs.Count == 0)
            {

                messageTextField.IsEnabled = false;
            }
            else
            {

                userDialogGrid.IsEnabled = true;
                userDialogGrid.ItemsSource = dialogs;
            }
            clearDialogButton.IsEnabled = false;
            sendTextButton.IsEnabled = false;
            messageTextField.IsEnabled = false;

        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            if (user.Role == (int)Role.ADMIN)
            {
                new AdminWindow(user).Show();
            }
            if (user.Role == (int)Role.REGISTERED)
            {
                new RegisterUserWindow(user).Show();
            }
            endDialogs = true;
            Close();

        }







        private void addInDialogs(object sender, RoutedEventArgs e)
        {
            string[] usersNames = registeredUsers.Text.Split(',');
            foreach (string s in usersNames)
            {
                DUser addedUser = (DUser)controller.complete(TextCommand.READ_USER_BY_NAME, s);
                if (addedUser != null && addedUser.Id != user.Id && !isExistInDialog(addedUser.Id))
                {

                    Message message = new Message("added you in dialog", user.Id, DateTime.Now);
                    UserMessage userMessage = new UserMessage();
                    MessageConclusionTime messageConclusionTime = (MessageConclusionTime)controller.complete(TextCommand.FIND_MESSAGE_CONCLUSION_BY_USERS_IDS, new int[] { user.Id, addedUser.Id });
                    if (messageConclusionTime == null)
                    {
                        messageConclusionTime = new MessageConclusionTime(user.Id, addedUser.Id, DateTime.Now, DateTime.Now);
                        controller.complete(TextCommand.CREATE_MESSAGE_CONCLUSION_TIME, messageConclusionTime);
                    }
                    else
                    {
                        if (user.Id == messageConclusionTime.FirstUserId && addedUser.Id == messageConclusionTime.SecondUserId)
                        {
                            controller.complete(TextCommand.UPDATE_MESSAGE_CONCLUSION_TIME, new object[] { messageConclusionTime.Id, new MessageConclusionTime(user.Id, addedUser.Id, DateTime.Now, messageConclusionTime.SecondUserDate) });
                        }
                        else
                        {
                            controller.complete(TextCommand.UPDATE_MESSAGE_CONCLUSION_TIME, new object[] { messageConclusionTime.Id, new MessageConclusionTime(addedUser.Id, user.Id, messageConclusionTime.SecondUserDate, DateTime.Now) });
                        }
                    }
                    message = (Message)controller.complete(TextCommand.CREATE_MESSAGE, message);

                    userMessage.MessageId = message.Id;
                    userMessage.UserGetterId = addedUser.Id;
                    controller.complete(TextCommand.CREATE_USER_MESSAGE, userMessage);

                    loadInUserDialogGrid(sender, e);
                }
            }
        }

        private bool isExistInDialog(int otherUserId)
        {
            List<Dialog> dialogs = (List<Dialog>)controller.complete(TextCommand.CREATE_USER_DIALOGS, user.Id);
            foreach (Dialog d in dialogs)
            {
                if (d.OtherUser.Id == otherUserId)
                {
                    return true;
                }
            }
            return false;
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            if (!messageTextField.Text.Trim().Equals(""))
            {
                Dialog dialog = (Dialog)userDialogGrid.SelectedItem;
                if (dialog != null)
                {
                    selectedDialog = dialog;
                }
                DUser otherUser = selectedDialog.OtherUser;
                Message message = new Message(messageTextField.Text, user.Id, DateTime.Now);
                UserMessage userMessage = new UserMessage();

                message = (Message)controller.complete(TextCommand.CREATE_MESSAGE, message);
                userMessage.MessageId = message.Id;
                userMessage.UserGetterId = otherUser.Id;
                controller.complete(TextCommand.CREATE_USER_MESSAGE, userMessage);

                messageTextField.Text = "";
                loadInUserDialogGrid(sender, e);
                selectedDialog = (Dialog)controller.complete(TextCommand.CREATE_USER_DIALOG, new DUser[] { user, otherUser });
                showUsersMessages(selectedDialog.UserMessage, otherUser);

            }
        }
        private void showUsersMessages(List<UserMessage> userMessages, DUser otherUser)
        {
            Message message = null;
            List<object> messageList = new List<object>();
            MessageConclusionTime messageConclusionTime = (MessageConclusionTime)controller.complete(TextCommand.FIND_MESSAGE_CONCLUSION_BY_USERS_IDS, new int[] { user.Id, otherUser.Id });
            DateTime dateTime;
            if (messageConclusionTime.FirstUserId == user.Id && messageConclusionTime.SecondUserId == otherUser.Id)
            {
                dateTime = messageConclusionTime.FirstUserDate;
            }
            else
            {
                dateTime = messageConclusionTime.SecondUserDate;
            }
            foreach (UserMessage um in userMessages)
            {

                if (um.UserGetterId == otherUser.Id)
                {

                    message = (Message)controller.complete(TextCommand.READ_MESSAGE_BY_ID, um.MessageId);
                    if (compareTimeForMessage(message.Date, dateTime))
                    {
                        messageList.Add(user);
                        messageList.Add(message);
                    }
                }
                else
                {
                    message = (Message)controller.complete(TextCommand.READ_MESSAGE_BY_ID, um.MessageId);
                    if (compareTimeForMessage(message.Date, dateTime))
                    {
                        messageList.Add(otherUser);
                        messageList.Add(message);
                    }
                }
            }

            Dispatcher.BeginInvoke(
            new ThreadStart(() =>
            clearDialogButton.IsEnabled = true)
            );
            Dispatcher.BeginInvoke(
            new ThreadStart(() =>
            sendTextButton.IsEnabled = true)
            );
            Dispatcher.BeginInvoke(
            new ThreadStart(() =>
            messageTextField.IsEnabled = true)
             );
            Dispatcher.BeginInvoke(
            new ThreadStart(() =>
            dialogList.ItemsSource = messageList)
            );

        }

        private void selectUserFoChat(object sender, SelectionChangedEventArgs e)
        {
            Thread checkUserBlock = new Thread(new ThreadStart(checkUserMessage));
            Dialog dialog = (Dialog)userDialogGrid.SelectedItem;
           


            if (dialog != null)
            {
                selectedDialog = dialog;
                
                checkUserBlock.Start();
            }

        }
        private void checkUserMessage()
        {
           
            while (!endDialogs)
            {
                selectedDialog = (Dialog)controller.complete(TextCommand.CREATE_USER_DIALOG, new DUser[] { user, selectedDialog.OtherUser });
                showUsersMessages(selectedDialog.UserMessage, selectedDialog.OtherUser);
                Thread.Sleep(1000);
            }
        }

        private void clearDiaolog(object sender, RoutedEventArgs e)
        {
            Dialog dialog = (Dialog)userDialogGrid.SelectedItem;
            if (dialog != null)
            {
                selectedDialog = dialog;
            }

            MessageConclusionTime messageConclusionTime = (MessageConclusionTime)controller.complete(TextCommand.FIND_MESSAGE_CONCLUSION_BY_USERS_IDS, new int[] { user.Id, selectedDialog.OtherUser.Id });
            controller.complete(TextCommand.UPDATE_MESSAGE_CONCLUSION_TIME, new object[] { messageConclusionTime.Id, new MessageConclusionTime(user.Id, selectedDialog.OtherUser.Id, DateTime.Now, messageConclusionTime.SecondUserDate) });

            loadInUserDialogGrid(sender, e);
        }
        private bool compareTimeForMessage(DateTime more, DateTime less)
        {
            double moreTime = more.Second + more.Minute * 60 + more.Hour * 60 * 60 + more.Day * 60 * 60 * 24 + more.Month * 60 * 60 * 24 * 30 + more.Year * 60 * 60 * 24 * 30 * 12;
            double lessTime = less.Second + less.Minute * 60 + less.Hour * 60 * 60 + less.Day * 60 * 60 * 24 + less.Month * 60 * 60 * 24 * 30 + less.Year * 60 * 60 * 24 * 30 * 12;
            return moreTime >= lessTime;
        }
    }
}
