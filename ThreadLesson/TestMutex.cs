using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLesson
{
    class TestMutex
    {
        static Mutex m1 = new Mutex(true, "StasMutex");

        public static void Start()
        {
            if (IsInstance() == true)
            {
                Console.WriteLine("New Instance created...");
            }
            else
            {
                Console.WriteLine("Instance already acquired...");
            }
            Console.ReadLine();
        }

        static bool IsInstance()
        {
            if (m1.WaitOne(50,false) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
