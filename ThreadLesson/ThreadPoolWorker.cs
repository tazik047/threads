using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLesson
{
    class ThreadPoolWorker
    {
        public static void Start()
        {
            ThreadPool.QueueUserWorkItem(Method1);

            Action<object> action = Method1;
            var asyncResult = action.BeginInvoke(null, (result) =>
            {
                // do something when method ended.
            }, null);
            action.EndInvoke(asyncResult);

            Task.Run(() => Method1(null)).Wait();
        }

        private static void Method1(object args)
        {
            Console.WriteLine("Thread #{0}, is from pool = {1}", 
                Thread.CurrentThread.ManagedThreadId, 
                Thread.CurrentThread.IsThreadPoolThread);
        }
    }
}
