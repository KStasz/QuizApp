using QuizApp.UserWindows;
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
using Domain.Models;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace QuizApp
{
    /// <summary>
    /// Logika interakcji dla klasy UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        public UserPanel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newGameConfig = App.ServiceProvider.GetRequiredService<NewGameConfig>();
            newGameConfig.Show();
            newGameConfig.Closed += NewGameConfig_Closed;
            this.Hide();
        }

        private void NewGameConfig_Closed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var resultWindow = App.ServiceProvider.GetRequiredService<UserResultWindow>();
            resultWindow.Show();
            resultWindow.Closed += ResultWindow_Closed;
            this.Hide();
        }

        private void ResultWindow_Closed(object sender, EventArgs e)
        {
            this.Show();
        }


    }
}
