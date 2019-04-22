using System;

namespace ConsoleApp2.Extensions
{
    internal static class DisposableExtensions
    {
        public static void DisposeIfNotNull(this IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
