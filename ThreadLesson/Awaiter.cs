using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLesson
{
    public class Awaiter
    {
        public static void Start()
        {
            Console.WriteLine("Wait completed task:");
            WaitAwaiter(true).Wait();
            Console.WriteLine("Wait not completed task:");
            WaitAwaiter(false).Wait();
        }

        private static async Task WaitAwaiter(bool isCompleted)
        {
            var t = new TestAwaiter(isCompleted);
            string res = await t;
            var stack = new StackTrace();
            Console.WriteLine(stack);
        }
    }

    public class TestAwaiter
    {
        private readonly bool _isCompleted;

        public TestAwaiter(bool isCompleted)
        {
            this._isCompleted = isCompleted;
        }

        public CustomAwaiter GetAwaiter()
        {
            return new CustomAwaiter(_isCompleted);
        }
    }

    public class CustomAwaiter : INotifyCompletion
    {
        public bool IsCompleted { get; set; }

        public CustomAwaiter(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
        public void OnCompleted(Action continuation)
        {
            // do something long
            Thread.Sleep(1000);
            continuation();
        }

        public string GetResult()
        {
            return "End";
        }
    }



    public struct AsyncStateMachine : IAsyncStateMachine
    {
        private int _state;
        CustomAwaiter _awaiter;
        //other variables

        public void MoveNext()
        {
            // Jump table to get back to the right statement upon resumption 
            switch (this._state)
            {
                case 1:
                    goto Label1;
            }

            var t = new TestAwaiter(true);
            this._awaiter = t.GetAwaiter();

            if (!this._awaiter.IsCompleted)
            {
                this._state = 1;
                this._awaiter.OnCompleted(MoveNext);
                return;

            }
            Label1:
            string result = this._awaiter.GetResult();
            var stack = new StackTrace();
            Console.WriteLine(stack);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            //set state machine
        }
    }
}
