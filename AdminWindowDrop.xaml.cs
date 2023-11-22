using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AcademicSysrem
{
    public partial class AdminWindowDrop : Window
    {
        private DataBase database;
        public AdminWindowDrop()
        {
            database = new DataBase();
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AdminWondowAdd adminWondowAdd = new AdminWondowAdd();
            adminWondowAdd.Show();
            this.Close();
        }

        private void Button_Click_Priskirti(object sender, RoutedEventArgs e)
        {
            AdminWindowPriskirt adminWindowPriskirt = new AdminWindowPriskirt();
            adminWindowPriskirt.Show();
            this.Close();
        }

        private void Button_Click_DropTeach(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();
            string login = LoginT.Text;

            try
            {
                conn.Open();

                string queryDropTeacher = $" DELETE FROM teacherssubjects WHERE idTeachers IN (SELECT idTeachers FROM teachers WHERE Login = @LoginT1);" +
                    $"\r\nDELETE FROM teachingassignments WHERE idTeachers IN (SELECT idTeachers FROM teachers WHERE Login = @LoginT2);" +
                    $"\r\nDELETE FROM teachers WHERE Login = @LoginT3;";

                MySqlCommand cmdDropT = new MySqlCommand(queryDropTeacher, conn);
                cmdDropT.Parameters.Add("@LoginT1", MySqlDbType.VarChar).Value = login;
                cmdDropT.Parameters.Add("@LoginT2", MySqlDbType.VarChar).Value = login;
                cmdDropT.Parameters.Add("@LoginT3", MySqlDbType.VarChar).Value = login;

                if (cmdDropT.ExecuteNonQuery() > 1)
                {
                    MessageBox.Show("Dėstytojas pašalintas");
                    LoginT.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Klaida!!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Button_Click_DropStud(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();
            string login = LoginS.Text;

            try
            {
                conn.Open();

                string queryDropStudent = $" DELETE FROM grades WHERE idStudents IN (SELECT idStudents FROM students WHERE Login = @LoginS1);" +
                    $"\r\nDELETE FROM students WHERE Login = @LoginS2;";

                MySqlCommand cmdDropS = new MySqlCommand(queryDropStudent, conn);
                cmdDropS.Parameters.Add("@LoginS1", MySqlDbType.VarChar).Value = login;
                cmdDropS.Parameters.Add("@LoginS2", MySqlDbType.VarChar).Value = login;

                if (cmdDropS.ExecuteNonQuery() > 1)
                {
                    MessageBox.Show("Studentas pašalintas");
                    LoginS.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Klaida!!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Button_Click_DropGroup(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();
            string group = GroupName.Text;

            try
            {
                conn.Open();

                string queryDropGroup = $" DELETE FROM students WHERE idGroups IN (SELECT idGroups FROM `groups` WHERE GroupName = @Gn1);" +
                    $"\r\nDELETE FROM teachingassignments WHERE idGroups IN (SELECT idGroups FROM `groups` WHERE GroupName = @Gn2);" +
                    $"\r\nDELETE FROM `groups` WHERE GroupName = @Gn3;";

                MySqlCommand cmdDropG = new MySqlCommand(queryDropGroup, conn);
                cmdDropG.Parameters.Add("@Gn1", MySqlDbType.VarChar).Value = group;
                cmdDropG.Parameters.Add("@Gn2", MySqlDbType.VarChar).Value = group;
                cmdDropG.Parameters.Add("@Gn3", MySqlDbType.VarChar).Value = group;

                if (cmdDropG.ExecuteNonQuery() > 1)
                {
                    MessageBox.Show("Grupė pašalinta");
                    group = string.Empty;
                }
                else
                {
                    MessageBox.Show("Klaida!!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Button_Click_DropSub(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();
            string subject = SubName.Text;

            try
            {
                conn.Open();

                string queryDropSub = $" DELETE FROM teachingassignments WHERE idSubjects IN (SELECT idSubjects FROM subjects WHERE SubjectName = @Sub1);" +
                    $"\r\nDELETE FROM subjects WHERE SubjectName = @Sub2;";

                MySqlCommand cmdDropSub = new MySqlCommand(queryDropSub, conn);
                cmdDropSub.Parameters.Add("@Sub1", MySqlDbType.VarChar).Value = subject;
                cmdDropSub.Parameters.Add("@Sub2", MySqlDbType.VarChar).Value = subject;

                if (cmdDropSub.ExecuteNonQuery() > 1)
                {
                    MessageBox.Show("Dalykas pašalintas");
                    SubName.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Klaida!!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
