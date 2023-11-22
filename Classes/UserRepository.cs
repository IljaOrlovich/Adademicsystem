using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicSysrem.Classes
{
    public class UserRepository
    {
        private DataBase database;

        public UserRepository(DataBase db)
        {
            database = db;
        }

        public User GetUserFromDatabase(string login)
        {
            MySqlConnection conn = database.GetConnection();

            try
            {
                conn.Open();

                string queryStudents = "SELECT * FROM students WHERE Login = @login";
                MySqlCommand cmdStudents = new MySqlCommand(queryStudents, conn);
                cmdStudents.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
                MySqlDataReader readerStudents = cmdStudents.ExecuteReader();

                if (readerStudents.Read())
                {
                    string userLogin = (string)readerStudents["Login"];
                    string userPassword = (string)readerStudents["Password"];

                    return new User(userLogin, userPassword, "student");
                }

                readerStudents.Close();

                string queryTeachers = "SELECT * FROM teachers WHERE Login = @login";
                MySqlCommand cmdTeachers = new MySqlCommand(queryTeachers, conn);
                cmdTeachers.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
                MySqlDataReader readerTeachers = cmdTeachers.ExecuteReader();

                if (readerTeachers.Read())
                {
                    string userLogin = (string)readerTeachers["Login"];
                    string userPassword = (string)readerTeachers["Password"];

                    return new User(userLogin, userPassword, "teacher");
                }

                readerTeachers.Close();

                string queryAdmin = "SELECT * FROM admin WHERE Login = @login";
                MySqlCommand cmdAdmin = new MySqlCommand(queryAdmin, conn);
                cmdAdmin.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
                MySqlDataReader readerAdmin = cmdAdmin.ExecuteReader();

                if (readerAdmin.Read())
                {
                    string userLogin = (string)readerAdmin["Login"];
                    string userPassword = (string)readerAdmin["Password"];

                    return new User(userLogin, userPassword, "admin");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }

            return null;
        }
    }
}
