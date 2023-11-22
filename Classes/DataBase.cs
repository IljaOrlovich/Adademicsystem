using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AcademicSysrem
{
    public class DataBase
    {
        private static string connection = "Server=MYSQL6008.site4now.net;Database=db_aa1de5_ais;Uid=aa1de5_ais;Pwd=Root123123";
           private MySqlConnection conn = new MySqlConnection(connection);

        public MySqlConnection GetConnection()
        {
            return conn;
        }
    }
}
