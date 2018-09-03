using H4Visita.Droid;
using System.IO;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace H4Visita.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteAsyncConnection DbConnection()
        {
            var dbName = "H4Visita.db3";
            var path = Path.Combine(System.Environment.
                GetFolderPath(System.Environment.
                SpecialFolder.Personal), dbName);
            return new SQLiteAsyncConnection(path);
        }
    }
}