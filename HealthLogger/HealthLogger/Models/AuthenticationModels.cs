using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger.Models
{
    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class RegisterModel
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
