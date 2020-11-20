using static System.Console;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleThreading
{
    public class SemaphoreMethods
    {
        private static Mutex mutex = new Mutex();

        public static void Method1(List<ThreadingObject> thread)
        {
            for (int i = 0; i <= thread.Count - 1; i++)
            {
                Program.semPool.WaitOne();

                WriteLine("Method1 is : {0}", i);
                thread[i].Name = (i + 1).ToString();

                Program.semPool.Release();
            }
        }

        public static void Method2(List<ThreadingObject> thread)
        {
            for (int t = 0; t <= thread.Count - 1; t++)
            {
                WriteLine("Method2 is : {0}", t);
                thread[t].MyBool = !thread[t].MyBool;
            }
        }
    }
}
