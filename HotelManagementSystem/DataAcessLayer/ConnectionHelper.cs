using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
    internal class ConnectionHelper
    {
        public static string GetConnectionString() //melhoria 
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\Users\entra21\Documents\HotelDB.mdf;Integrated Security=True;
            Connect Timeout=10";
        }
    }
}
