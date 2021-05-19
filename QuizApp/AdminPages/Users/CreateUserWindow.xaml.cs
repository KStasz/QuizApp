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

namespace QuizApp.AdminPages.Users
{
    /// <summary>
    /// Logika interakcji dla klasy CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow()
        {
            InitializeComponent();
        }

        public string username { get; private set; }
        public string password { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Username.Text.Length >= 3 && txt_Password.Text.Length >= 6)
            {
                this.DialogResult = true;
                username = txt_Username.Text;
                password = txt_Password.Text;
            }
            else
            {
                this.DialogResult = false;
                MessageBox.Show("Nazwa użytkownika musi zawierać minimum 3 znaki oraz hasło musi zawierać minimum 6 znaków", "Niepoprawna nazwa użytkownika", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
