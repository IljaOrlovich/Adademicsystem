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
    /// <summary>
    /// Interaction logic for AdminWondowAdd.xaml
    /// </summary>
    public partial class AdminWondowAdd : Window
    {
        private DataBase database;
        public AdminWondowAdd()
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

        private void Button_Click_Drop(object sender, RoutedEventArgs e)
        {
            AdminWindowDrop adminWindowDrop = new AdminWindowDrop();
            adminWindowDrop.Show();
            this.Close();
        }

        private void Button_Click_Priskirti(object sender, RoutedEventArgs e)
        {
            AdminWindowPriskirt adminWindowPriskirt = new AdminWindowPriskirt();
            adminWindowPriskirt.Show();
            this.Close();
        }

        private void Button_Click_AddTeach(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();


            try
            {
                conn.Open();

                string queryAddTeach = $"insert into teachers (Teacher_First_Name, Teacher_Last_Name, Login, Password, idAdmin) values (@FNameT, @LNameT, @LoginT, @Pswrd, 1);";

                MySqlCommand cmdAddT = new MySqlCommand(queryAddTeach, conn);
                cmdAddT.Parameters.Add("@FNameT", MySqlDbType.VarChar).Value = FNameT.Text;
                cmdAddT.Parameters.Add("@LNameT", MySqlDbType.VarChar).Value = LNameT.Text;
                cmdAddT.Parameters.Add("@LoginT", MySqlDbType.VarChar).Value = LoginT.Text;
                cmdAddT.Parameters.Add("@Pswrd", MySqlDbType.VarChar).Value = LNameT.Text;

                if (cmdAddT.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Dėstytojas sėkmingai sukurtas");
                    FNameT.Text = string.Empty;
                    LNameT.Text = string.Empty;
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

        private void Button_Click_AddStud(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();


            try
            {
                conn.Open();

                string queryAddStud = $"insert into students (Student_First_Name, Student_Last_Name, Login, Password, idGroups, idAdmin,idAssignments) values (@FNameS, @LNameS, @LoginS, @Pswrd, 5, 1, 14);";

                MySqlCommand cmdAddS = new MySqlCommand(queryAddStud, conn);
                cmdAddS.Parameters.Add("@FNameS", MySqlDbType.VarChar).Value = FNameS.Text;
                cmdAddS.Parameters.Add("@LNameS", MySqlDbType.VarChar).Value = LNameS.Text;
                cmdAddS.Parameters.Add("@LoginS", MySqlDbType.VarChar).Value = LoginS.Text;
                cmdAddS.Parameters.Add("@Pswrd", MySqlDbType.VarChar).Value = LNameS.Text;

                if (cmdAddS.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Studentas sėkmingai sukurtas");
                    FNameS.Text = string.Empty;
                    LNameS.Text = string.Empty;
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

        private void Button_Click_AddSub(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();


            try
            {
                conn.Open();

                string queryAddSub = $"insert into subjects (SubjectName, idAdmin) values (@SubName, 1);";

                MySqlCommand cmdAddSub = new MySqlCommand(queryAddSub, conn);
                cmdAddSub.Parameters.Add("@SubName", MySqlDbType.VarChar).Value = SubName.Text;

                if (cmdAddSub.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Dalykas sėkmingai sukurtas");
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

        private void Button_Click_AddGroup(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = database.GetConnection();


            try
            {
                conn.Open();

                string queryAddGroup = $"insert into `groups` (GroupName, idAdmin) values (@GroupName, 1);";

                MySqlCommand cmdAddG = new MySqlCommand(queryAddGroup, conn);
                cmdAddG.Parameters.Add("@GroupName", MySqlDbType.VarChar).Value = GroupName.Text;

                if (cmdAddG.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Grupė sėkmingai sukurta");
                    GroupName.Text = string.Empty;
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
