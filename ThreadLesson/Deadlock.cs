using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLesson
{
    public class Deadlock
    {
        private static readonly object _locker1= new object();
        private static readonly object _locker2 = new object();

        public static void Start()
        {
            Console.WriteLine("Start wait");
            
            var thread1 = new Thread(Method1);
            var thread2 = new Thread(Method2);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            // AsyncDeadlock().Wait();
        }

        private static void Method1()
        {
            lock (_locker1)
            {
                Thread.Sleep(500);
                lock (_locker2)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Deadlock");
                }
                Console.WriteLine("Never called");
            }
        }

        private static void Method2()
        {
            lock (_locker2)
            {
                Thread.Sleep(1000);

                Monitor.Enter(_locker1);
                try
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Deadlock");
                }
                finally
                {
                    Monitor.Exit(_locker1);
                }

                Console.WriteLine("Never called");
            }
        }

        private static async Task AsyncDeadlock()
        {
            await Task.Delay(1000);
        }
    }
}
