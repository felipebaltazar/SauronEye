using System;
using System.Threading.Tasks;

namespace Saruman.Helpers.Extensions
{
    public static class TaskExtensions
    {
        public async static void FireAndForgetSafe(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }
    }
}
