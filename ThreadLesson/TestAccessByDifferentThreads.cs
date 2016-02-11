using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLesson
{
    class TestAccessByDifferentThreads
    {
        public static void Start()
        {
            var t= new TestAccessByDifferentThreads();
            var threads = new Task[10];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Task(t.S1);
                threads[i].Start();
            }
            Task.WaitAll(threads);

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Task(t.S2);
                threads[i].Start();
            }
            Task.WaitAll(threads);

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Task(t.S3);
                threads[i].Start();
            }
            Task.WaitAll(threads);

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Task(t.S4);
                threads[i].Start();
            }
            Task.WaitAll(threads);

            Console.WriteLine(t.res1);
            Console.WriteLine(t.res2);
            Console.WriteLine(t.res3);
            Console.WriteLine(t.res4);
        }

        private int res1 = 0;

        private object _locker = new object();
        private int res2 = 0;

        private volatile int res3 = 0;

        private int res4 = 0;

         void S1()
        {
            for (int i = 0; i < 1000; i++)
            {
                res1 = res1 + 1;
            }
        }

        void S2()
        {
            for (int i = 0; i < 1000; i++)
            {
                lock (_locker)
                {
                    res2 = res2 + 1;
                }
            }
        }

        void S3()
        {
            for (int i = 0; i < 1000; i++)
            {
                res3 = res3 + 1;
            }
        }

        void S4()
        {
            for (int i = 0; i < 1000; i++)
            {
                Interlocked.Add(ref res4, 1);
            }
        }
    }
}
