using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens
{
    internal static class Calculator
    {
        public static async Task<long> Calculate(int n, CancellationToken token)
        {
            long sum = 0;
            var taskFactory = new TaskFactory(token);
            await taskFactory.StartNew(() =>
            {

                for (var i = 0; i < n; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    // i + 1 is to allow 2147483647 (Max(Int32)) 
                    sum = sum + (i + 1);
                }

            }, token);

            return sum;
        }
    }
}
