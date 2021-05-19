using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using QuizApp.AdminPages;
using QuizApp.ViewModelHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizApp.ViewModels
{
    public class QuestionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        private ObservableCollection<AnswerModel> _avilableAnswers = new ObservableCollection<AnswerModel>();
        public ObservableCollection<AnswerModel> AvilableAnswers
        {
            get
            {
                return _avilableAnswers;
            }
            set
            {
                _avilableAnswers = value;
                NotifyPropertyChanged(nameof(AvilableAnswers));
            }
        }

        private AnswerModel _selectedAnswer;
        public AnswerModel SelectedAnswer
        {
            get
            {
                return _selectedAnswer;
            }
            set
            {
                _selectedAnswer = value;
                NotifyPropertyChanged(nameof(SelectedAnswer));
            }
        }


        private List<QuestionModel> _avilableQuestions;
        public List<QuestionModel> AvilableQuestions
        {
            get
            {
                return _avilableQuestions;
            }
            set
            {
                _avilableQuestions = value;
                NotifyPropertyChanged(nameof(AvilableQuestions));
            }
        }

        private QuestionModel _selectedQuestion;
        public QuestionModel SelectedQuestion
        {
            get
            {
                return _selectedQuestion;
            }
            set
            {
                _selectedQuestion = value;
                GetAnswers(SelectedQuestion);
                SetSelectedCategory(SelectedQuestion);
                NotifyPropertyChanged(nameof(SelectedQuestion));
            }
        }


        private List<CategoryModel> _avilableCategories;
        public List<CategoryModel> AvilableCategories
        {
            get
            {
                return _avilableCategories;
            }
            set
            {
                _avilableCategories = value;
                NotifyPropertyChanged(nameof(AvilableCategories));
            }
        }

        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                if (SelectedQuestion != null)
                    SelectedQuestion.AssignCategory(SelectedCategory);
                NotifyPropertyChanged(nameof(SelectedCategory));
            }
        }

        private string _questionName;
        public string QuestionName
        {
            get
            {
                return _questionName;
            }
            set
            {
                _questionName = value;
                NotifyPropertyChanged(nameof(QuestionName));
            }
        }

        #endregion

        #region ButtonProperties

        private ICommand _saveQuestion;
        public ICommand SaveQuestion
        {
            get
            {
                return _saveQuestion;
            }
            private set { }
        }

        private ICommand _createQuestion;
        public ICommand CreateQuestion
        {
            get
            {
                return _createQuestion;
            }
            private set { }
        }

        private ICommand _removeQuestion;
        public ICommand RemoveQuestion
        {
            get
            {
                return _removeQuestion;
            }
            set { }
        }

        private ICommand _removeAnswer;
        public ICommand RemoveAnswer
        {
            get
            {
                return _removeAnswer;
            }
            set { }
        }

        private ICommand _addAnswer;
        public ICommand AddAnswer
        {
            get
            {
                return _addAnswer;
            }
            set { }
        }

        #endregion

        #region Fields
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IAnswerRepository _answerRepository;
        #endregion

        #region Private Methods
        private void SetInitialData()
        {
            GetCategories();
            GetQuestions();
            _saveQuestion = new RelayCommand(x => SaveQuestion_Clicked());
            _createQuestion = new RelayCommand(x => CreateQuestion_Clicked());
            _removeQuestion = new RelayCommand(x => RemoveQuestion_Clicked());
            _addAnswer = new RelayCommand(x => AddAnswer_Clicked());
            _removeAnswer = new RelayCommand(x => RemoveAnswer_Clicked());
        }

        private void GetQuestions()
        {
            AvilableQuestions = _questionsRepository.ListOfQuestions();
        }

        private void GetCategories()
        {
            AvilableCategories = _categoriesRepository.GetAllCategories();
        }

        private void GetAnswers(QuestionModel selectedQuestion)
        {
            if (SelectedQuestion != null)
            {
                AvilableAnswers = new ObservableCollection<AnswerModel>(_answerRepository.ListOfAnswers(SelectedQuestion.Id));
            }
        }

        private void SetSelectedCategory(QuestionModel selectedQuestion)
        {
            if (selectedQuestion != null)
            {
                SelectedCategory = selectedQuestion.assignedCategory;
            }
        }

        private void RemoveAnswer_Clicked()
        {
            _answerRepository.RemoveAnswer(SelectedAnswer);
            AvilableAnswers = new ObservableCollection<AnswerModel>(_answerRepository.ListOfAnswers(SelectedQuestion.Id));
        }

        private void AddAnswer_Clicked()
        {
            var answerAddWindow = new AnswerAddWindow();
            answerAddWindow.ShowDialog();
            var answer = answerAddWindow.GetAnswer();
            answer.QuestionId = SelectedQuestion.Id;
            if (answer != null)
            {
                _answerRepository.AddAnswer(answer);
                AvilableAnswers = new ObservableCollection<AnswerModel>(_answerRepository.ListOfAnswers(SelectedQuestion.Id));
            }
        }

        private void RemoveQuestion_Clicked()
        {
            _questionsRepository.RemoveQuestion(SelectedQuestion);
            GetQuestions();
            AvilableAnswers = new ObservableCollection<AnswerModel>();
            SelectedCategory = new CategoryModel() { Name = "" };
        }

        private void CreateQuestion_Clicked()
        {
            var createQuestion = new CreateQuestionWindow();
            createQuestion.ShowDialog();
            if (createQuestion.DialogResult == true)
            {
                QuestionModel model = new QuestionModel();
                model.Name = createQuestion.QuestionName;
                var id = _questionsRepository.AddQuestion(model);
                AvilableQuestions = _questionsRepository.ListOfQuestions();
                SelectedQuestion = _questionsRepository.GetQuestion(id);
            }
        }

        private void SaveQuestion_Clicked()
        {
            _questionsRepository.UpdateQuestion(SelectedQuestion);
            GetQuestions();
            AvilableAnswers = new ObservableCollection<AnswerModel>();
            SelectedCategory = new CategoryModel() { Name = "" };
        }
        #endregion

        #region Public Methods
        #endregion

        #region Constructor
        public QuestionsViewModel(IAnswerRepository answerRepository, ICategoriesRepository categoriesRepository, IQuestionsRepository questionsRepository)
        {
            _answerRepository = answerRepository;
            _categoriesRepository = categoriesRepository;
            _questionsRepository = questionsRepository;
            SetInitialData();
        }

        #endregion
    }
}
