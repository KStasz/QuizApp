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

        public void CreateUser(ApplicationUsers user)
        {
            string sql = $"INSERT INTO ApplicationUsers (Username, Name, Surname, Password) " +
                $"VALUES (@username, @name, @surname, @password)";
            _sqlDataAccess.SaveData(sql, new
            {
                username = user.Username,
                name = user.Name,
                surname = user.Surname,
                password = user.Password
            });
        }

        public void DeleteUser(ApplicationUsers user)
        {
            string sql = "DELETE FROM ApplicationUsers WHERE Id = @id";
            _sqlDataAccess.SaveData(sql, new { id = user.Id });
        }

        public ApplicationUserRoles GetRole(int roleId)
        {
            string sql = "SELECT Id, RoleName FROM ApplicationUserRoles WHERE Id = @id";
            return _sqlDataAccess.LoadData<ApplicationUserRoles, dynamic>(sql, new { id = roleId }).FirstOrDefault();
        }

        public List<ApplicationUserRoles> GetRoles()
        {
            string sql = "SELECT Id, RoleName FROM ApplicationUserRoles";
            return _sqlDataAccess.LoadData<ApplicationUserRoles, dynamic>(sql, new { });
        }

        public ApplicationUsers GetUser(ApplicationUsers user)
        {
            string sql = "SELECT Id, Username, Name, Surname, Password, RoleId, IsActive, CreationDate FROM ApplicationUsers WHERE Id = @id";
            return _sqlDataAccess.LoadData<ApplicationUsers, dynamic>(sql, new
            {
                id = user.Id
            }).FirstOrDefault();
        }

        public List<ApplicationUsers> ListOfUsers()
        {
            string sql = "SELECT Id, Username, Name, Surname, Password, RoleId, IsActive, CreationDate FROM ApplicationUsers";
            return _sqlDataAccess.LoadData<ApplicationUsers, dynamic>(sql, new { });
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

        public void UpdateUser(ApplicationUsers user)
        {
            string sql = $"UPDATE ApplicationUsers " +
                $"SET Username = @username, " +
                $"Name = @name, " +
                $"Surname = @surname, " +
                $"Password = @password, " +
                $"RoleId = @roleId " +
                $"WHERE Id = @id";
            _sqlDataAccess.SaveData(sql, new
            {
                username = user.Username,
                name = user.Name,
                surname = user.Surname,
                password = user.Password,
                roleId = user.RoleId,
                id = user.Id
            });
        }
    }
}
