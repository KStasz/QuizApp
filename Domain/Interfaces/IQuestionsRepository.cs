using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IQuestionsRepository
    {
        List<QuestionModel> ListOfQuestions();
        List<QuestionModel> ListOfQuestions(int CategoryId);
        QuestionModel GetQuestion(int QuestionId);
        void AddQuestion(QuestionModel item);
        void RemoveQuestion(QuestionModel item);
        void UpdateQuestion(QuestionModel item);
    }
}
