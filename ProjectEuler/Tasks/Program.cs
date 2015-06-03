using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        public static void Main(string[] args)
        {
            Task task1 = new Task(new Action(printMessage));
            Task task2 = new Task(delegate { printMessage(); });
            Task task3 = new Task(() => printMessage());
            Task task4 = new Task(() => { printMessage(); });

            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();

            Task<Int32> t = new Task<Int32>(n => Sum((Int32)n), 1000);
            t.Start();

            Task ctw = t.ContinueWith(task => Console.WriteLine("The sum is: "
                + task.Result));

            Console.WriteLine("Main method complete. Press <enter> to finish.");
            Console.ReadLine();
        }

        private static void printMessage()
        {
            Console.WriteLine("Hello, world!");
        }

        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
                checked { sum += n; }

            return sum;
        }
    }
}
