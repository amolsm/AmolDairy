using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanjilERP.Areas.Admin.Models
{
    public class UserVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int EmpId { get; set; }
        public string RoleId { get; set; }
    }
}