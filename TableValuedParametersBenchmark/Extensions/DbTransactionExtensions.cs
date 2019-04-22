using System.Data.Common;

namespace ConsoleApp2.Extensions
{
    internal static class DbTransactionExtensions
    {
        public static void RollbackIfNotNull(this DbTransaction dbTransaction)
        {
            if (dbTransaction != null)
            {
                dbTransaction.Rollback();
            }
        }
    }
}
