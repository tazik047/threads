using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLesson
{
    public class CancelTask
    {
        public static void Start()
        {
            TestException();
        }

        private static void TestException()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> task = Task.Run(() => TestTask(cts.Token), cts.Token);

            Thread.Sleep(100);

            cts.Cancel();

            try
            {
                task.Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("Cathc:");
                Console.WriteLine(ae.InnerException.Message);
            }
        }

        private static int TestTask(CancellationToken ct)
        {
            Thread.Sleep(1000);

            ct.ThrowIfCancellationRequested();

            return 100;
        }
    }
}
