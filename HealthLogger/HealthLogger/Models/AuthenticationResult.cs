using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger.Models
{

    public class RegisterResult
    {
        public string status { get; set; }
        public string message { get; set; }
    }
    public class LoginResult
    {
        public string token { get; set; }
        public DateTime validTo { get; set; }
        public string id { get; set; }
    }
        
}
