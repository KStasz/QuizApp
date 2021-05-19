using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;

namespace DataAccess.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;
        private readonly ICategoriesRepository _categoriesRepository;

        public QuestionsRepository(ISqlDataAccess sqlDataAccess, ICategoriesRepository categoriesRepository)
        {
            _sqlDataAccess = sqlDataAccess;
            _categoriesRepository = categoriesRepository;
        }
        public int AddQuestion(QuestionModel item)
        {
            string sql = "INSERT INTO Questions (Name) VALUES (@name);SELECT last_insert_rowid();";
            return _sqlDataAccess.LoadData<int, dynamic>(sql, new { name = item.Name }).FirstOrDefault();
        }

        public QuestionModel GetQuestion(int QuestionId)
        {
            string sql = "SELECT Id, Name, QuestionContent, CategoryId, IsActive, CreationDate FROM Questions WHERE Id = @id";
            var result = _sqlDataAccess.LoadData<QuestionModel, dynamic>(sql, new { id = QuestionId }).ToList();
            if (result != null && result.Count > 0)
            {
                result.FirstOrDefault().AssignCategory(_categoriesRepository.GetCategory(result.FirstOrDefault().CategoryId));
                return result.FirstOrDefault();
            }
            return new QuestionModel();
        }

        public List<QuestionModel> ListOfQuestions()
        {
            string sql = "SELECT Id, Name, QuestionContent, CategoryId, IsActive, CreationDate FROM Questions";
            var result = _sqlDataAccess.LoadData<QuestionModel, dynamic>(sql, new { });
            if (result != null && result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].assignedCategory = _categoriesRepository.GetCategory(result[i].CategoryId);
                }
                return result;
            }
            return new List<QuestionModel>();
        }

        public List<QuestionModel> ListOfQuestions(int CategoryId)
        {
            string sql = "SELECT Id, Name, QuestionContent, CategoryId, IsActive, CreationDate FROM Questions WHERE CategoryId = @id";
            var result = _sqlDataAccess.LoadData<QuestionModel, dynamic>(sql, new { id = CategoryId });
            if (result != null && result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].assignedCategory = _categoriesRepository.GetCategory(result[i].CategoryId);
                }
                return result;
            }
            return new List<QuestionModel>();
        }

        public void RemoveQuestion(QuestionModel item)
        {
            string sql = "DELETE FROM Questions WHERE Id = @id";
            _sqlDataAccess.SaveData(sql, new { id = item.Id });
        }

        public void UpdateQuestion(QuestionModel item)
        {
            string sql = "UPDATE Questions SET Name = @name, QuestionContent = @content, CategoryId = @cid WHERE Id = @id";
            _sqlDataAccess.SaveData(sql, new { name = item.Name, content = item.QuestionContent, cid = item.CategoryId, id = item.Id });
        }
    }
}
