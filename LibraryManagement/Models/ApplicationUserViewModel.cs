using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string RoleId { get; set; }

        public string RoleName { get; set; }
        public string Password { get; set; }
    }
}