using DataAccess.Repositories;
using Domain.Models;
using Domain.StaticMembers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplicationUserRepository userRepository = new ApplicationUserRepository();
            UserLoginState.LogIn(userRepository.LogIn(txt_UserName.Text, txt_Password.Password));
            if (UserLoginState.LoggedInState)
            {
                if (UserLoginState.loggedUser.role.RoleName.ToLower() == "administrator")
                {
                    var adminPanel = new AdminPanel();
                    adminPanel.Show();
                    Close();
                }
                else
                {
                    var userPanel = new UserPanel();
                    userPanel.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Nie odnaleziono użytkownika, lub podane hasło jest nieprawidłowe", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
