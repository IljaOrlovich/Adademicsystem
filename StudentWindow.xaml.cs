using AcademicSysrem.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class StudentWindow : Window
    {
        private string login;
        public Student Student = new Student();
        public StudentWindow(Student student)
        {
            InitializeComponent();
            Student = student;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
                Student student11 = new Student();
                this.login = student11.GetLogin();

                DataBase database = new DataBase();
                MySqlConnection conn = database.GetConnection();

                try
                {
                    conn.Open();

                    string queryGrades = $"SELECT s.SubjectName AS \"Dalyko pavadinimas\", " +
                    $"t.Teacher_First_Name AS \"Dėstytojo vardas\", " +
                    $"t.Teacher_Last_Name AS \"Dėstytojo Pavardė\", " +
                    $"g.Value AS \"Pažymis\" \r\nFROM grades g \r\n" +
                    $"JOIN students st ON g.idStudents = st.idStudents  \r\n" +
                    $"JOIN teachingassignments ta ON g.idAssignments = ta.idAssignments\r\n" +
                    $"JOIN subjects s ON ta.idSubjects = s.idSubjects\r\n" +
                    $"JOIN teachers t ON ta.idTeachers = t.idTeachers\r\n" +
                    $"WHERE st.Login = @login;";

                    MySqlCommand cmdGrades = new MySqlCommand(queryGrades, conn);
                    cmdGrades.Parameters.Add("@login", MySqlDbType.VarChar).Value = Student.GetLogin();
                    MySqlDataAdapter LoadGrades = new MySqlDataAdapter(cmdGrades);

                    DataTable table = new DataTable();
                    LoadGrades.Fill(table);

                    Grades.ItemsSource = table.DefaultView;
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
