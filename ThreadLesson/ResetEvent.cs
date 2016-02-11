using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLesson
{
    public class ResetEvent
    {
        public static void Start()
        {
            Bad();
            // Good();
        }

        private static bool _startWork = false;

        private static void Bad()
        {
            var t = new Thread(DoBadPractices);
            
            t.Start();
            Console.ReadLine();

            _startWork = true;

            Console.ReadLine();
        }

        private static void DoBadPractices()
        {
            Console.WriteLine("Press any Key for executing");
            while (!_startWork)
            {
                continue;
            }
            Console.WriteLine("Do something...");
        }

        private static readonly AutoResetEvent AutoResetEvent = 
            new AutoResetEvent(false);

        private static void Good()
        {
            var t = new Thread(Do);

            t.Start();
            Console.ReadLine();

            AutoResetEvent.Set();

            Console.ReadLine();
        }

        private static void Do()
        {
            Console.WriteLine("Press any Key for executing");
            AutoResetEvent.WaitOne();
            Console.WriteLine("Do something...");
        }
    }
}
