using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public CategoriesRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<CategoryModel> GetAllCategories()
        {
            string sql = "SELECT Id, Name, IsActive, CreationDate FROM Categories WHERE IsActive = 1";
            var result = _sqlDataAccess.LoadData<CategoryModel, dynamic>(sql, new { });
            if (result != null && result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].assignedQuestions = GetCategoryQuestions(result[i].Id);
                }
                return result;
            }
            return new List<CategoryModel>();
        }

        public CategoryModel GetCategory(int Id)
        {
            string sql = "SELECT Id, Name, IsActive, CreationDate FROM Categories WHERE Id = @Id";
            var result = _sqlDataAccess.LoadData<CategoryModel, dynamic>(sql, new { Id = Id });
            if (result != null && result.Count == 1)
                return result.FirstOrDefault();
            return new CategoryModel();
        }

        public List<QuestionModel> GetCategoryQuestions(int CategoryId)
        {
            string sql = "SELECT Id, Name, QuestionContent, CategoryId, IsActive, CreationDate FROM Questions WHERE CategoryId = @id";
            var result = _sqlDataAccess.LoadData<QuestionModel, dynamic>(sql, new { id = CategoryId });
            if (result != null && result.Count > 0)
                return result;
            return new List<QuestionModel>();
        }
    }
}
