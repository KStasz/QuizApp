using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using QuizApp.ViewModelHelpers;
using System.Windows;
using QuizApp.UserWindows;

namespace QuizApp.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged, ICloseable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<EventArgs> RequestClose;

        protected void NotifyPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        #region StaticProperties
        public static int gameId { get; set; }
        #endregion

        #region Fields

        private readonly IGameRepository _gameRepository;
        private readonly IResultsRepository _resultsRepository;
        #endregion

        #region Properties

        private GameQuestion _actualQuestion;
        public GameQuestion ActualQuestion
        {
            get
            {
                return _actualQuestion;
            }
            set
            {
                _actualQuestion = value;
                AnswerModels = new ObservableCollection<AnswerModel>(ActualQuestion.Question.Answers);
                NotifyPropertyChanged(nameof(ActualQuestion));
            }
        }

        private ObservableCollection<AnswerModel> _answerModels = new ObservableCollection<AnswerModel>();
        public ObservableCollection<AnswerModel> AnswerModels
        {
            get
            {
                return _answerModels;
            }
            set
            {
                _answerModels = value;
                NotifyPropertyChanged(nameof(AnswerModels));
            }
        }

        private GameHeader _gameHeader;
        public GameHeader GameHeader
        {
            get
            {
                return _gameHeader;
            }
            set
            {
                _gameHeader = value;
                var x = GameHeader.Qustions.FirstOrDefault(x => x.UserAnswerId == 0);
                if (x != null)
                {
                    ActualQuestion = x;
                }
                else
                {
                    EndGame();
                }
                NotifyPropertyChanged(nameof(GameHeader));
            }
        }
        #endregion

        #region PrivateMethods

        private void SetInitialData()
        {
            GameHeader = _gameRepository.GetGameHeader(gameId);
            _btnNextQuestion = new RelayCommand(x => BtnNextQuestion_Click());
        }

        private void BtnNextQuestion_Click()
        {
            var answers = AnswerModels.Where(x => x.IsSelected == true).ToList();
            if (answers.Count > 0)
            {
                ActualQuestion.Answer = answers.FirstOrDefault();
                _gameRepository.SendAnswer(ActualQuestion);
                GameHeader = _gameRepository.GetGameHeader(gameId);
            }
        }

        private void EndGame()
        {
            _gameRepository.FinishGame(GameHeader);
            var result = GameHeader.Qustions.SelectMany(x => x.Question.Answers.Where(y => y.Id == x.UserAnswerId).ToList()).ToList();
            MessageBox.Show($"Poprawne odpowiedzi: {result.Where(x => x.IsCorrect == true).Count()} z {result.Count()}");
            RequestClose.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region ButtonProperties

        private ICommand _btnNextQuestion;
        public ICommand BtnNextQuestion
        {
            get
            {
                return _btnNextQuestion;
            }
            private set { }
        }


        #endregion

        #region Constructor
        public GameViewModel(IGameRepository gameRepository, IResultsRepository resultsRepository)
        {
            _gameRepository = gameRepository;
            _resultsRepository = resultsRepository;
            SetInitialData();
        }
        #endregion
    }
}
