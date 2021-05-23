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
using QuizApp.ViewModels;

namespace QuizApp.UserWindows
{
    /// <summary>
    /// Logika interakcji dla klasy NewGameConfig.xaml
    /// </summary>
    public partial class NewGameConfig : Window
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IQuestionsRepository _questionsRepository;

        private bool showCategoryCombobox { get; set; } = false;
        private string selectedQuizType { get; set; } = String.Empty;


        public NewGameConfig(IGameRepository gameRepository, ICategoriesRepository categoriesRepository, IQuestionsRepository questionsRepository)
        {
            _gameRepository = gameRepository;
            _categoriesRepository = categoriesRepository;
            _questionsRepository = questionsRepository;
            InitializeComponent();
            cmb_Categories.ItemsSource = new ObservableCollection<CategoryModel>(_categoriesRepository.GetAllCategories());
            cmb_Categories.DisplayMemberPath = "Name";
            cmb_QuestionNumber.ItemsSource = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            cmb_QuestionNumber.SelectedItem = 1;
            ComboboxDisabler();
        }

        private void btn_StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (txt_GameName.Text.Length >= 3)
            {
                List<QuestionModel> gameQuestions;
                var newGame = _gameRepository.CreateGame(new GameHeader() { Name = txt_GameName.Text, UserId = UserLoginState.loggedUser.Id });
                if (selectedQuizType == "Wylosuj pytania z jednej kategorii")
                {
                    var selectedCategory = (CategoryModel)cmb_Categories.SelectedItem;
                    var questions = _questionsRepository.ListOfQuestions(selectedCategory.Id);
                    gameQuestions = GetQuestions((int)cmb_QuestionNumber.SelectedItem, questions);
                    newGame.Qustions = CreateGameQuestions(newGame, gameQuestions);
                    _gameRepository.SetGameQuestions(newGame);
                    RunGame(newGame.Id);
                }
                else if(selectedQuizType == "Wylosuj pytania z różnych kategorii")
                {
                    var questions = _questionsRepository.ListOfQuestions();
                    gameQuestions = GetQuestions((int)cmb_QuestionNumber.SelectedItem, questions);
                    newGame.Qustions = CreateGameQuestions(newGame, gameQuestions);
                    _gameRepository.SetGameQuestions(newGame);
                    RunGame(newGame.Id);
                }
                else
                {
                    MessageBox.Show("Nie wybrano rodzaju gry");
                }
            }
        }

        private void RunGame(int gameId)
        {
            GameViewModel.gameId = gameId;
            var gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }

        private List<GameQuestion> CreateGameQuestions(GameHeader game, List<QuestionModel> gameQuestions)
        {
            List<GameQuestion> locallist = new List<GameQuestion>();
            foreach (var item in gameQuestions)
            {
                locallist.Add(new GameQuestion()
                {
                    GameId = game.Id,
                    Game = game,
                    QuestionId = item.Id
                });
            }
            return locallist;
        }

        private List<QuestionModel> GetQuestions(int questionCount, List<QuestionModel> questions)
        {
            HashSet<QuestionModel> locallist = new HashSet<QuestionModel>(new QuestionsComparer());
            Random rnd = new Random();
            while (locallist.Count < questionCount)
            {
                int questionNumber = rnd.Next(0, questions.Count);
                locallist.Add(questions[questionNumber]);
            }
            return locallist.ToList();
        }

        private void ComboboxDisabler()
        {
            if (showCategoryCombobox == false)
            {
                cmb_Categories.IsEnabled = false;
                cmb_Categories.SelectedItem = null;
            }
            else
            {
                cmb_Categories.IsEnabled = true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)cmb_GameType.SelectedItem;
            selectedQuizType = (string)item.Content;
            if (selectedQuizType == "Wylosuj pytania z jednej kategorii")
            {
                showCategoryCombobox = true;
                ComboboxDisabler();
            }
            else
            {
                showCategoryCombobox = false;
                ComboboxDisabler();
            }
        }
    }
}
