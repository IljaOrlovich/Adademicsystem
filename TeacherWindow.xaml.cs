using AcademicSysrem.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class TeacherWindow : Window
    {
        public Teacher Teacher = new Teacher();
        private string login;
        private DataBase database;
        public TeacherWindow(Teacher teacher)
        {
            InitializeComponent();
            database = new DataBase();
            Teacher = teacher;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Group_Loaded(object sender, RoutedEventArgs e)
        {
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();

            try
            {
                conn.Open();

                string queryGrades = $"SELECT * from `groups`;";

                MySqlCommand cmdGrades = new MySqlCommand(queryGrades, conn);
                MySqlDataReader reader = cmdGrades.ExecuteReader();

                while (reader.Read())
                {
                    Group.Items.Add(reader["GroupName"].ToString());
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

        private void Students_Loaded(object sender, RoutedEventArgs e)
        {

            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();
            if (Group.SelectedItem != null)
            {
                string selectedgroup = Group.SelectedItem.ToString();
                string group = selectedgroup;
                try
                {
                    conn.Open();

                    string queryStudents = $"select concat(Student_First_Name, ' ',Student_Last_Name)" +
                        $"as Studentas from students where idGroups = " +
                        $"(select idGroups from `groups` " +
                        $"where GroupName=@gr);";

                    MySqlCommand cmdStudents = new MySqlCommand(queryStudents, conn);
                    cmdStudents.Parameters.Add("@gr", MySqlDbType.VarChar).Value = group;
                    MySqlDataReader reader = cmdStudents.ExecuteReader();

                    while (reader.Read())
                    {
                        Students.Items.Add(reader["Studentas"].ToString());
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

        private void Group_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();
            if (Group.SelectedItem != null)
            {
                string selectedgroup = Group.SelectedItem.ToString();
                string group = selectedgroup;

                try
                {
                    conn.Open();

                    string queryStudents = $"select concat(Student_First_Name, ' ',Student_Last_Name)" +
                        $"as Studentas from students where idGroups = " +
                        $"(select idGroups from `groups` " +
                        $"where GroupName=@gr);";

                    MySqlCommand cmdStudents = new MySqlCommand(queryStudents, conn);
                    cmdStudents.Parameters.Add("@gr", MySqlDbType.VarChar).Value = group;
                    MySqlDataReader reader = cmdStudents.ExecuteReader();
                    Students.Items.Clear();
                    while (reader.Read())
                    {
                        Students.Items.Add(reader["Studentas"].ToString());
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

        private void Subject_Loaded(object sender, RoutedEventArgs e)
        {
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();



            try
            {
                conn.Open();

                string queryTeacherSub = $"SELECT t.Teacher_First_Name AS TeacherName, s.SubjectName AS \"Dalyko pavadinimas\" " +
                    $"FROM teacherssubjects ts  " +
                    $"JOIN teachers AS t ON ts.idTeachers = t.idTeachers " +
                    $"JOIN teachingassignments as tas ON ts.idAssignments = tas.idAssignments " +
                    $"JOIN subjects AS s ON tas.idSubjects = s.idSubjects " +
                    $"WHERE ts.idTeachers = (SELECT idTeachers FROM teachers WHERE Login = @tus);";

                MySqlCommand cmdStudents = new MySqlCommand(queryTeacherSub, conn);
                cmdStudents.Parameters.Add("@tus", MySqlDbType.VarChar).Value = Teacher.GetLogin();
                MySqlDataReader reader = cmdStudents.ExecuteReader();
                Subject.Items.Clear();
                while (reader.Read())
                {
                    Subject.Items.Add(reader["Dalyko pavadinimas"].ToString());
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

        private void Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_Teacher(object sender, RoutedEventArgs e)
        {
            string firstword = "";
            string secondword = "";
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();

            if (Students.SelectedItem != null)
            {
                string selectedItem = Students.SelectedItem.ToString();


                string[] parts = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 2)
                {
                    firstword = parts[0];
                    secondword = parts[1];

                }

            }

            if (Subject.SelectedItem != null)
            {
                string S = Subject.SelectedItem.ToString();
            }

            try
            {
                conn.Open();

                string queryGradeAdd = $"INSERT INTO grades (Value, idAdmin, idStudents, idAssignments) VALUES (@Grd, 1, (SELECT idStudents FROM students WHERE Login = (SELECT Login FROM students WHERE Student_First_Name = @F AND Student_Last_Name = @L)), (SELECT idAssignments FROM students WHERE Student_First_Name = @F1 AND Student_Last_Name = @L1));";

                MySqlCommand cmdStudents1 = new MySqlCommand(queryGradeAdd, conn);
                cmdStudents1.Parameters.Add("@F", MySqlDbType.VarChar).Value = firstword;
                cmdStudents1.Parameters.Add("@L", MySqlDbType.VarChar).Value = secondword;
                cmdStudents1.Parameters.Add("@F1", MySqlDbType.VarChar).Value = firstword;
                cmdStudents1.Parameters.Add("@L1", MySqlDbType.VarChar).Value = secondword;
                cmdStudents1.Parameters.Add("@Grd", MySqlDbType.Int32).Value = Grds.Text;
                int rowsAffected = cmdStudents1.ExecuteNonQuery();
                Subject.Items.Clear();

                if (cmdStudents1.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Pažymis sėkmingai įkeltas");
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GradeRedag_Loaded(object sender, RoutedEventArgs e)
        {
            string firstword = "";
            string secondword = "";
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();

            if (Students.SelectedItem != null)
            {
                string selectGradeRedag = Students.SelectedItem.ToString();
            }

            if (Students.SelectedItem != null)
            {
                string selectedItem = Students.SelectedItem.ToString();


                string[] parts = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 2)
                {
                    firstword = parts[0];
                    secondword = parts[1];

                }

            }

            try
            {
                conn.Open();

                string queryGradesRedag = $"select  concat(idGrades,'-', Value) AS ASD from grades where idStudents = (select idStudents from students where Student_First_Name = @StudF and Student_Last_Name = @StudL)";

                MySqlCommand cmdStudents = new MySqlCommand(queryGradesRedag, conn);
                cmdStudents.Parameters.Add("@StudF", MySqlDbType.VarChar).Value = firstword;
                cmdStudents.Parameters.Add("@StudL", MySqlDbType.VarChar).Value = secondword;
                MySqlDataReader reader = cmdStudents.ExecuteReader();
                GradeRedag.Items.Clear();
                while (reader.Read())
                {
                    GradeRedag.Items.Add(reader["ASD"].ToString());
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

        private void GradeRedag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Students_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GradeRedag_Loaded(sender, e);

        }


         private void Button_Click_Redag(object sender, RoutedEventArgs e)
         {
            string firstword = "";
            DataBase database = new DataBase();
             MySqlConnection conn = database.GetConnection();

            if (GradeRedag.SelectedItem != null)
            {
                string selectedItem = GradeRedag.SelectedItem.ToString();


                string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 2)
                {
                    firstword = parts[0];

                }

            }

             try
             {
                 conn.Open();

                 string queryGradeRedag = $"update grades set Value = @Red where idGrades = @Fr";

                 MySqlCommand cmdStudents = new MySqlCommand(queryGradeRedag, conn);
                 cmdStudents.Parameters.Add("@Fr", MySqlDbType.VarChar).Value = firstword;
                 cmdStudents.Parameters.Add("@Red", MySqlDbType.Int32).Value = Redagavimas.Text;
                 GradeRedag.Items.Clear();

                 if (cmdStudents.ExecuteNonQuery() == 1)
                 {
                     MessageBox.Show("Pažymis sėkmingai įkeltas");
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