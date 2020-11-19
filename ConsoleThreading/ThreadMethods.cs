using System.Collections.Generic;
using ConsoleThreading;
using static System.Console;

// A simple class with two methods, which writes out a string and changes a variable
public static class ThreadMethods
{
    public static void Method1(List<ThreadingObject> thread)
    {
        for (int i = 0; i <= thread.Count - 1; i++)
        {
            WriteLine("Method1 is : {0}", i);
            thread[i].Name = (i + 1).ToString();
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