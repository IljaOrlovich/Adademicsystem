using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicSysrem.Classes
{
       public class User
        {
        public User()
        {

        }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User(string login, string password, string role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public bool Authenticate(string enteredPassword)
            {
                return Password == enteredPassword;
            }
        public void SetLogin(string login)
        {
            this.Login = login;
        }
        public string GetLogin()
        {
            return this.Login;
        }

        }

    
}
