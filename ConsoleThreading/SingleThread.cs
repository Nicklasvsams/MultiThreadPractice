using System.Collections.Generic;
using ConsoleThreading;
using static System.Console;

public static class SingleThread
{
    // static method one 
    public static void Method1(List<ThreadingObject> thread)
    {
        for (int i = 0; i <= thread.Count-1; i++)
        {
            WriteLine("Method1 is : {0}", i);
            thread[i].Name = (i + 1).ToString();
        }
    }

    // static method two 
    public static void Method2(List<ThreadingObject> thread)
    {
        for (int t = 0; t <= thread.Count - 1; t++)
        {
            WriteLine("Method2 is : {0}", t);
            thread[t].Name = (t + 1).ToString();
        }
    }
}