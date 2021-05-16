using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoriesRepository
    {
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int Id);
        List<QuestionModel> GetCategoryQuestions(int CategoryId);
    }
}
