using Domain.Models;
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
    /// Logika interakcji dla klasy AnswerAddWindow.xaml
    /// </summary>
    public partial class AnswerAddWindow : Window
    {
        public AnswerAddWindow()
        {
            InitializeComponent();
        }

        private AnswerModel answer { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            answer = new AnswerModel();
            answer.IsCorrectAnswer = (bool)check_IsCorrect.IsChecked;
            answer.Content = txt_answerContent.Text;
            this.Close();
        }

        public AnswerModel GetAnswer()
        {
            return answer;
        }
    }
}
