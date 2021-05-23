using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Domain.Interfaces;
using Domain.Models;
using Domain.StaticMembers;

namespace QuizApp.UserWindows
{
    /// <summary>
    /// Logika interakcji dla klasy UserResultWindow.xaml
    /// </summary>
    public partial class UserResultWindow : Window
    {
        private readonly IResultsRepository _resultsRepository;

        public ObservableCollection<UserResultsModel> result { get; set; } = new ObservableCollection<UserResultsModel>();

        public UserResultWindow(IResultsRepository resultsRepository)
        {
            _resultsRepository = resultsRepository;
            result = new ObservableCollection<UserResultsModel>(_resultsRepository.GetUserResult(UserLoginState.loggedUser.Id));
            InitializeComponent();
            DataContext = this;
        }

    }
}
