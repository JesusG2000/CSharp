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
using Music.db;

namespace Music.view
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private Controller controller;

        private const string NAME = "Name";
        private const string PASSWORD = "Password";
        private const string CONFIRM_PASSWORD = "ConfirmPassword";

        private RegistrateValidation validation;
        private string name;
        private string password;
        private string confirmPassword;

        public Registration()
        {
            validation = new RegistrateValidation();
            controller = new Controller();
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void backToWelcome(object sender, RoutedEventArgs e)
        {
            new Welcome().Show();
            Close();

        }

        private void registrateUser(object sender, RoutedEventArgs e)
        {
            password = Password.Password;
            confirmPassword = ConfirmPassword.Password;
            if (validation.isEquals(password, confirmPassword)&&password.Length>=4 && password.Length<=16)
            {
                DUser user = new DUser(name, password, Role.REGISTERED);
                if (!(bool)controller.complete(TextCommand.IS_USER_REGISTERED, user))
                {
                    controller.complete(TextCommand.CREATE_USER, user);
                    backToWelcome(sender, e);
                }
                else
                {
                    MessageBox.Show("This user is already registered");
                }
            }
            else
            {
                MessageBox.Show("Passwords doesnt match or too long password");
            }
        }

        private void checkValidation(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            name = validation.userNameValidation(textBox) ? textBox.Text : "";
           

            if (validation.isAllFieldValidate())
            {

                Registrate.IsEnabled = true;
            }
            else
            {
                Registrate.IsEnabled = false;
            }

        }
    }
}
