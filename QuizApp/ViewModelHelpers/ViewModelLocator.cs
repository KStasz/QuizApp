using Microsoft.Extensions.DependencyInjection;
using QuizApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.ViewModelHelpers
{
    public class ViewModelLocator
    {
        public QuestionsViewModel questionsViewModel => App.ServiceProvider.GetRequiredService<QuestionsViewModel>();
    }
}
