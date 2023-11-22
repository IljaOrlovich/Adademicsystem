using AcademicSysrem.Classes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AcademicSysrem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserRepository userRepository;

        public MainWindow()
        {
            InitializeComponent();
            DataBase database = new DataBase();
            userRepository = new UserRepository(database);
        }

       

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string enteredLogin = Login.Text;
            string enteredPassword = Password.Password;

            User user = userRepository.GetUserFromDatabase(enteredLogin);

            if (user != null)
            {
                if (user.Authenticate(enteredPassword))
                {
                    if (user.Role =="admin")
                    { 
                    Admin1 admin1 = new Admin1();
                    admin1.Show();
                    Admin admin11 = new Admin(enteredLogin, enteredPassword);
                        admin11.SetLogin(enteredLogin);
                    this.Close();
                    }
                    if (user.Role == "student")
                    {
                        Student student11 = new Student(enteredLogin, enteredPassword);
                        student11.SetLogin(enteredLogin);
                        StudentWindow student = new StudentWindow(student11);
                        student.Show();
                        
                        this.Close();
                    }
                    if (user.Role == "teacher")
                    {
                        var teacher11 = new AcademicSysrem.Classes.Teacher(enteredLogin, enteredPassword);
                        teacher11.SetLogin(enteredLogin);
                        TeacherWindow teacher = new TeacherWindow(teacher11);
                        teacher.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Neteisingas slaptažodis");
                }
            }
            else
            {
                MessageBox.Show("Tokio userio nėra");
            }
        }
    }
}
