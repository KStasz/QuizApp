using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public AnswerRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public void AddAnswer(AnswerModel item)
        {
            string sql = "INSERT INTO Answers (AnswerContent, QuestionId, IsCorrect) VALUES (@answerContent, @questionId, @isCorrect);";
            _sqlDataAccess.SaveData(sql, new
            {
                answerContent = item.AnswerContent,
                questionId = item.QuestionId,
                isCorrect = item.IsCorrect
            });
        }

        public AnswerModel GetAnswer(int Id)
        {
            string sql = "SELECT Id, AnswerContent, QuestionId, IsCorrect, IsActive, CreationDate FROM Answers WHERE Id = @id";
            var result = _sqlDataAccess.LoadData<AnswerModel, dynamic>(sql, new { id = Id }).ToList();
            if (result != null && result.Count > 0)
                return result.FirstOrDefault();
            return new AnswerModel();
        }

        public List<AnswerModel> ListOfAnswers(int QuestionId)
        {
            string sql = "SELECT Id, AnswerContent, QuestionId, IsCorrect, IsActive, CreationDate FROM Answers WHERE QuestionId = @questionId";
            return _sqlDataAccess.LoadData<AnswerModel, dynamic>(sql, new { questionId = QuestionId });
        }

        public List<AnswerModel> ListOfAnswers()
        {
            string sql = "SELECT Id, AnswerContent, QuestionId, IsCorrect, IsActive, CreationDate FROM Answers";
            return _sqlDataAccess.LoadData<AnswerModel, dynamic>(sql, new { });
        }

        public void RemoveAnswer(AnswerModel item)
        {
            string sql = "DELETE FROM Answers WHERE Id = @id;";
            _sqlDataAccess.SaveData(sql, new { id = item.Id });
        }
    }
}
