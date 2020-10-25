using Music.dto;
using Music.controller;
using Music.service;
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
using System.IO;
using Music.db;

namespace Music.view
{
    /// <summary>
    /// Логика взаимодействия для Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        private Controller controller;
        //private IUserService userService = ServiceFactory.getInstance().GetUserService();

        private LoginValidation validation;
        private const string LOGIN = "Login";
        private const string PASSWORD = "Password";


        private string login;
        private string password;
        public Welcome()
        {
            validation = new LoginValidation();
            controller = new Controller();
         
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void registrateUser(object sender, RoutedEventArgs e)
        {
            new Registration().Show();
            Close();

        }



        private void checkValidation(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            login = validation.userNameValidation(textBox) ? textBox.Text : "";

            if (validation.isAllFieldValidate())
            {

                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private void loginUser(object sender, RoutedEventArgs e)
        {
            password = Password.Password;
            DUser user = new DUser(login, password);

            if ((bool)controller.complete(TextCommand.IS_USER_EXIST, user))
            {
                user = (DUser)controller.complete(TextCommand.READ_USER_BY_NAME, user.Login);
               // MessageBox.Show("Welcome");
                if (user.Role == (int)Role.REGISTERED)
                {
                    new RegisterUserWindow(user).Show();
                }
                if (user.Role == (int)Role.ADMIN)
                {
                    new AdminWindow(user).Show();
                }
               
                Close();
            }
            else
            {
                MessageBox.Show("Wrong login or password");
            }
        }

        private void goToCasualMode(object sender, RoutedEventArgs e)
        {
            using (TestDbContext context = new TestDbContext())
            {
                
            }
            new CasualWindow().Show();
            Close();
        }





       
    }
}
