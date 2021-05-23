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

namespace QuizApp.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                ActualQuestion = GameHeader.Qustions.First();
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
            MessageBox.Show("Działa");
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
            private set {  }
        }

        #endregion

        #region Constructor
        public GameViewModel(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
            SetInitialData();
        }
        #endregion
    }
}
