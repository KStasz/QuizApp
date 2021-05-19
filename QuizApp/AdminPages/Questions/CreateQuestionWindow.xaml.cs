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

namespace QuizApp.AdminPages
{
    /// <summary>
    /// Logika interakcji dla klasy CreateQuestionWindow.xaml
    /// </summary>
    public partial class CreateQuestionWindow : Window
    {
        public CreateQuestionWindow()
        {
            InitializeComponent();
        }

        public string QuestionName { get; private set; }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (txt_QuestionName.Text.Length >= 3)
            {
                this.DialogResult = true;
                QuestionName = txt_QuestionName.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nazwa pytania musi posiadać minimum 3 znaki", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
