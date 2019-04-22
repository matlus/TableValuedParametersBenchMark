using System.Data.Common;

namespace ConsoleApp2.Extensions
{
    internal static class DbConnectionExtensions
    {
        public static void CloseAndDispose(this DbConnection dbConnection)
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }
    }
}
