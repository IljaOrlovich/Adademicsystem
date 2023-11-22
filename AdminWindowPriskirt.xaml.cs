using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Xml;
using System.Xml.Linq;

namespace AcademicSysrem
{
    public partial class AdminWindowPriskirt : Window
    {
        private DataBase database;
        public AdminWindowPriskirt()
        {

            InitializeComponent();
            database = new DataBase();
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

        private void Button_Click_Drop(object sender, RoutedEventArgs e)
        {
            AdminWindowDrop adminWindowDrop = new AdminWindowDrop();
            adminWindowDrop.Show();
            this.Close();
        }

        private void Pasirinkimas_Loaded(object sender, RoutedEventArgs e)
        {
           

        }

        private void Students_Loaded(object sender, RoutedEventArgs e)
        {

            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();

            try
            {
                conn.Open();

                string queryStudents = $"select concat(Student_First_Name, ' ',Student_Last_Name)" +
                    $"as Studentas from students;";

                MySqlCommand cmdStudents = new MySqlCommand(queryStudents, conn);
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

        private void Groups_Loaded(object sender, RoutedEventArgs e)
        {
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();

            try
            {
                conn.Open();

                string queryGroups = $"select GroupName from `groups`;";

                MySqlCommand cmdGroups = new MySqlCommand(queryGroups, conn);
                MySqlDataReader reader = cmdGroups.ExecuteReader();

                while (reader.Read())
                {
                    Groups.Items.Add(reader["GroupName"].ToString());
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

        

        private void Pasirinkimas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Pasirinkimas.SelectedItem is ComboBoxItem selectedItem)
            {
                string str = selectedItem.Content.ToString();

                if (str == "Studentas")
                {
                    Stud.Visibility = Visibility.Visible;
                    Student.Visibility = Visibility.Visible;
                    Grupe.Visibility = Visibility.Visible;
                    Groups.Visibility = Visibility.Visible;
                    Students.Visibility = Visibility.Visible;

                    Teachers.Visibility = Visibility.Hidden;
                    Subjects.Visibility = Visibility.Hidden;
                    Teach.Visibility = Visibility.Hidden;
                    Sub.Visibility = Visibility.Hidden;
                    Teacher.Visibility = Visibility.Hidden;

                }
                else if (str == "Dėstytojas")
                {
                    Teachers.Visibility = Visibility.Visible;
                    Subjects.Visibility = Visibility.Visible;
                    Teach.Visibility = Visibility.Visible;
                    Sub.Visibility = Visibility.Visible;
                    Teacher.Visibility = Visibility.Visible;

                    Stud.Visibility = Visibility.Hidden;
                    Student.Visibility = Visibility.Hidden;
                    Grupe.Visibility = Visibility.Hidden;
                    Groups.Visibility = Visibility.Hidden;
                    Students.Visibility = Visibility.Hidden;
                }

            }
        }

        private void Subjects_Loaded(object sender, RoutedEventArgs e)
        {
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();

            try
            {
                conn.Open();

                string querySubjects = $"select SubjectName from subjects;";

                MySqlCommand cmdSubject = new MySqlCommand(querySubjects, conn);
                MySqlDataReader reader = cmdSubject.ExecuteReader();

                while (reader.Read())
                {
                   Subjects.Items.Add(reader["SubjectName"].ToString());
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

        private void Teachers_Loaded(object sender, RoutedEventArgs e)
        {
            DataBase database = new DataBase();
            MySqlConnection conn = database.GetConnection();

            try
            {
                conn.Open();

                string queryTeachers = $"select concat(Teacher_First_Name, ' ',Teacher_Last_Name)" +
                    $"as Teacher from teachers;";

                MySqlCommand cmdTeacher = new MySqlCommand(queryTeachers, conn);
                MySqlDataReader reader = cmdTeacher.ExecuteReader();

                while (reader.Read())
                {
                    Teachers.Items.Add(reader["Teacher"].ToString());
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
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {/*
            string firstword = "";
            string secondword = "";
            MySqlConnection conn = database.GetConnection();


            if (Teachers.SelectedItem != null)
            {
                string selectedItem = Teachers.SelectedItem.ToString();


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

                string queryAddTeach = $"insert into teachingassignments (idGroups, idTeachers, idSubjects, idAdmin) Values (5, (select idTeachers from teachers where Teacher_First_Name = @T1 and Teacher_Last_Name = @T2), (select idSubjects from subjects where SubjectName = @Subj), 1); " +
                    $"insert into teacherssubjects (idTeachers, idAssignments, Admin_idAdmin) Values ((select idTeachers from teachers where Teacher_Frist_Name = @T1 and Teachers_Last_Name = @T2), 12, 1););";

                MySqlCommand cmdAddT = new MySqlCommand(queryAddTeach, conn);
                cmdAddT.Parameters.Add("@T1", MySqlDbType.VarChar).Value = firstword;
                cmdAddT.Parameters.Add("@Subj", MySqlDbType.VarChar).Value = Subjects.Text;
                cmdAddT.Parameters.Add("@T2", MySqlDbType.VarChar).Value = secondword;

                if (cmdAddT.ExecuteNonQuery() > 1)
                {
                    MessageBox.Show("Sėkmingai");

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
            }*/

        }

        private void Button_Click_Save1(object sender, RoutedEventArgs e)
        {

        }
    }
}
