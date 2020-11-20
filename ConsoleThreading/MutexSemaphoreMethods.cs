﻿using static System.Console;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleThreading
{
    public class MutexSemaphoreMethods
    {
        private static Mutex mutex = new Mutex();
        public static int counter = 0;
        public static int counter2 = 0;

        public static void Method1(List<ThreadingObject> thread)
        {
            for (int i = 0; i <= thread.Count - 1; i++)
            {
                Program.SemPool.WaitOne();
                mutex.WaitOne();
                counter++;
                mutex.ReleaseMutex();
                Program.SemPool.Release();

                WriteLine("Method1 is : {0}", i);
                thread[i].Name = (i + 1).ToString();
            }
        }

        public static void Method2(List<ThreadingObject> thread)
        {
            for (int t = 0; t <= thread.Count - 1; t++)
            {
                counter2++;
                WriteLine("Method2 is : {0}", t);
                thread[t].MyBool = !thread[t].MyBool;
            }
        }
    }
}
