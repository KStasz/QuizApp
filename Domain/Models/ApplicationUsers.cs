using System;

namespace Domain.Models
{
    public class ApplicationUsers
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public ApplicationUserRoles role { get; set; }
    }
}
