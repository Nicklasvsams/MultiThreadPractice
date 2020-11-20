using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ConsoleThreading;

public class Program
{
    public static Semaphore SemPool = new Semaphore(0, 2);

    static public void Main()
    {
        // Initilasing lists to hold objects and threads
        List<Thread> threadList = new List<Thread>();
        List<Thread> threadListMutex = new List<Thread>();
        List<Thread> threadListMutexSemaphore = new List<Thread>();
        List<ThreadingObject> threadingObjectList = new List<ThreadingObject>();


        // Initialises objects to be used in the thread example - Adds them to a list
        for (int i = 0; i < 500; i++)
        {
            var myObject = new ThreadingObject("Name" + i, i, true, new List<KeyValuePair<int, string>>());
            threadingObjectList.Add(myObject);
        }

        // Creates new thread processes referring to the multithread class methods and adds them to a list
        for (int i = 0; i < 50; i++)
        {
            threadList.Add(new Thread(() => ThreadMethods.Method1(threadingObjectList)));
            threadList.Add(new Thread(() => ThreadMethods.Method2(threadingObjectList)));

            threadListMutex.Add(new Thread(() => MutexMethods.Method1(threadingObjectList)));
            threadListMutex.Add(new Thread(() => MutexMethods.Method2(threadingObjectList)));

            threadListMutexSemaphore.Add(new Thread(() => MutexSemaphoreMethods.Method1(threadingObjectList)));
            threadListMutexSemaphore.Add(new Thread(() => MutexSemaphoreMethods.Method2(threadingObjectList)));
        }

        // Creates a stopwatch, in order for us to check how long processing takes
        Stopwatch sw = new Stopwatch();

        // Starts the stopwatch
        //sw.Start();

        //// For each of the objects in the object list, we run method 1 and 2 in the SingleThread class
        //for (int i = 0; i < threadingObjectList.Count; i++)
        //{
        //    ThreadMethods.Method1(threadingObjectList);
        //    ThreadMethods.Method2(threadingObjectList);
        //}

        //// Stops the stopwatch and shows elapsed time
        //sw.Stop();
        //Console.WriteLine("Time elapsed for singlethreading: " + sw.Elapsed);
        //Console.ReadKey();

        //// Program sleeps for specified amount of time in ms
        //Thread.Sleep(10000);

        Console.WriteLine("\n--- Now doing multithreading ---");
        Thread.Sleep(1000);

        // Stopwatch is restarted
        sw.Start();

        // For each of the threads created, start the thread process
        // Join means wait for processes to finished, before continuing
        for (int i = 0; i < threadList.Count; i++)
        {
            threadList[i].Start();
            threadList[i].Join();
        }

        // Stops the stopwatch and shows elapsed time
        sw.Stop();
        Console.WriteLine("Time elapsed for multithreading: " + sw.Elapsed);
        Console.ReadKey();

        Thread.Sleep(1000);

        Console.WriteLine("\n --- Now starting mutex multithread example ---");
        Thread.Sleep(1000);

        sw.Restart();

        for (int i = 0; i < threadList.Count; i++)
        {
            threadListMutex[i].Start();
        }


        for (int i = 0; i < threadList.Count; i++)
        {
            threadListMutex[i].Join();
        }

        sw.Stop();

        Console.WriteLine("Time elapsed for multithreading with Mutex: " + sw.Elapsed);
        Console.WriteLine("Count with mutex: " + MutexMethods.counter);
        Console.WriteLine("Count without mutex: " + MutexMethods.counter2);
        Console.ReadKey();

        Thread.Sleep(1000);

        Console.WriteLine("\n --- Now starting mutex & semaphore multithread example ---");
        Thread.Sleep(1000);

        sw.Restart();

        for (int i = 0; i < threadList.Count; i++)
        {
            threadListMutexSemaphore[i].Start();
        }

        SemPool.Release();

        for (int i = 0; i < threadList.Count; i++)
        {
            threadListMutexSemaphore[i].Join();
        }

        sw.Stop();


        Console.WriteLine("Time elapsed for multithreading with Mutex & Semaphore: " + sw.Elapsed);


        Console.ReadKey();
    }
}