using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public ApplicationUserRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public ApplicationUsers LogIn(string username, string password)
        {
            string sql = "SELECT Id, Username, Name, Surname, Password, RoleId, IsActive, CreationDate FROM ApplicationUsers WHERE Username = @Username AND Password = @Password AND IsActive = 1";
            var user = _sqlDataAccess.LoadData<ApplicationUsers, dynamic>(sql, new { Username = username, Password = password }).FirstOrDefault();
            if (user != null)
            {
                sql = "SELECT Id, RoleName FROM ApplicationUserRoles WHERE Id = @userRoleId";
                user.role = _sqlDataAccess.LoadData<ApplicationUserRoles, dynamic>(sql, new { userRoleId = user.RoleId }).FirstOrDefault();
                return user;
            }
            return null;
        }
    }
}
