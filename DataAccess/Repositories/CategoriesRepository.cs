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
        private readonly SqlDataAccessRepository _sqlDataAccess;

        public CategoriesRepository()
        {
            _sqlDataAccess = new SqlDataAccessRepository();
        }

        public List<CategoryModel> GetAllCategories()
        {
            string sql = "SELECT Id, Name, IsActive, CreationDate FROM Categories WHERE IsActive = 1";
            return _sqlDataAccess.LoadData<CategoryModel, dynamic>(sql, new { });
        }
    }
}
