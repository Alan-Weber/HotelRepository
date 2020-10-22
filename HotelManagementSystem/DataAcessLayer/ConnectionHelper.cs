namespace DataAcessLayer {
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
