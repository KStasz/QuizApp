using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.StaticMembers
{
    public static class UserLoginState
    {
        public static ApplicationUsers loggedUser { get; set; }
        public static bool LoggedInState { get; set; } = false;

        public static void LogIn(ApplicationUsers user)
        {
            if (user != null)
            {
                loggedUser = user;
                LoggedInState = true;
            }
        }

        public static void LogOut()
        {
            loggedUser = null;
            LoggedInState = false;
        }
    }
}
