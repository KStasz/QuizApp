using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IApplicationUserRepository
    {
        ApplicationUsers LogIn(string username, string password);
        void UpdateUser(ApplicationUsers user);
        void CreateUser(ApplicationUsers user);
        void DeleteUser(ApplicationUsers user);
        List<ApplicationUsers> ListOfUsers();
        ApplicationUsers GetUser(ApplicationUsers user);
        ApplicationUserRoles GetRole(int roleId);
        List<ApplicationUserRoles> GetRoles();
    }
}
