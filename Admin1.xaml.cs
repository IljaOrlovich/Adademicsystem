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
    /// Interaction logic for Admin1.xaml
    /// </summary>
    public partial class Admin1 : Window
    {
        public Admin1()
        {
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

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AdminWondowAdd adminWondowAdd = new AdminWondowAdd();
            adminWondowAdd.Show();
            this.Close();
        }
    }
}
