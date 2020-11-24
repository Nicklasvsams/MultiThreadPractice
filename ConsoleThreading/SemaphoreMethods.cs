using static System.Console;
using System.Threading;

namespace ConsoleThreading
{
    public class SemaphoreMethods
    {
        public static void Method1()
        {
            WriteLine("Thread {0} = waiting", Thread.CurrentThread.Name);
            Program.semPool.WaitOne();
            WriteLine("Thread {0} begins!", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            WriteLine("Thread {0} releasing...", Thread.CurrentThread.Name);
            Program.semPool.Release();
        }
    }
}
