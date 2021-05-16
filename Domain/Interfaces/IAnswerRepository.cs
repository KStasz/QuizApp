using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAnswerRepository
    {
        List<AnswerModel> ListOfAnswers();
        List<AnswerModel> ListOfAnswers(int QuestionId);
        AnswerModel GetAnswer(int Id);
        void AddAnswer(AnswerModel item);
        void RemoveAnswer(AnswerModel item);
    }
}
