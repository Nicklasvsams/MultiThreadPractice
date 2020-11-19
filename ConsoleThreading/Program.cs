// C# program to illustrate the  
// concept of single threaded model 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ConsoleThreading;


// Driver Class 
public class GFG
{

    // Main Method 
    static public void Main()
    {
        List<Thread> threadList = new List<Thread>();
        List<ThreadingObject> threadingObjectList = new List<ThreadingObject>();

        for (int i = 0; i < 500; i++)
        {
            var myObject = new ThreadingObject("Name" + i, i, true, new List<KeyValuePair<int, string>>());
            threadingObjectList.Add(myObject);
        }

        for (int i = 0; i < 100; i++)
        {
            threadList.Add(new Thread(() => MultiThread.Method1(threadingObjectList)));
            threadList.Add(new Thread(() => MultiThread.Method2(threadingObjectList)));
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < threadingObjectList.Count; i++)
        {
            SingleThread.Method1(threadingObjectList);
            SingleThread.Method2(threadingObjectList);
        }
        sw.Stop();
        Console.WriteLine("Time elapsed for singlethreading:" + sw.Elapsed);
        Thread.Sleep(10000);

        Console.WriteLine("\n--- Now doing multithreading ---");
        Thread.Sleep(1000);

        sw.Restart();


        for (int i = 0; i < threadList.Count; i++)
        {
            threadList[i].Start();
            threadList[i].Join();
        }


        sw.Stop();
        Console.WriteLine("Time elapsed for multithreading:" + sw.Elapsed);
    }
}