using QuizApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace QuizApp.AdminPages.Questions
{
    /// <summary>
    /// Logika interakcji dla klasy QuestionsSettings.xaml
    /// </summary>
    public partial class QuestionsSettings : Page
    {
        public QuestionsViewModel viewModel
        {
            set { DataContext = value; }
        }

        public QuestionsSettings()
        {
            InitializeComponent();
        }
    }
}
