using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ConsoleThreading;

public class Program
{
    public static Semaphore semPool;
    private static List<Thread> threadList = new List<Thread>();
    private static List<Thread> threadListMutex = new List<Thread>();
    private static List<Thread> threadListSemaphore = new List<Thread>();
    private static List<ThreadingObject> threadingObjectList = new List<ThreadingObject>();
    private static readonly Stopwatch sw = new Stopwatch();
    private static bool run = true;

    static public void Main()
    {
        do
        {
            Setup();
            Console.Clear();

            Console.WriteLine("1: Single thread\n2: Multithread\n3: Mutex multithread\n4: Semaphore multithread\n\nESC: Exit");
            var choice = Console.ReadKey();

            switch (choice.Key)
            {
                case ConsoleKey.D1:
                    SingleThread();
                    break;
                case ConsoleKey.D2:
                    MultiThread();
                    break;
                case ConsoleKey.D3:
                    MutexThread();
                    break;
                case ConsoleKey.D4:
                    SemaphoreThread();
                    break;
                case ConsoleKey.Escape:
                    run = false;
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
        while (run);
    }

    private static void SingleThread()
    {
        Console.WriteLine("\n--- Now doing singlethreading ---");
        Thread.Sleep(1000);

        //Starts the stopwatch
        sw.Start();

        // For each of the objects in the object list, we run method 1 and 2 in the SingleThread class
        for (int i = 0; i < threadingObjectList.Count; i++)
        {
            ThreadMethods.Method1(threadingObjectList);
            ThreadMethods.Method2(threadingObjectList);
        }

        sw.Stop();
        Console.WriteLine("Time elapsed for singlethreading: " + sw.Elapsed);
        sw.Reset();
    }

    private static void MultiThread()
    {
        Console.WriteLine("\n--- Now doing multithreading ---");
        Thread.Sleep(1000);

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
        sw.Reset();
    }

    private static void MutexThread()
    {
        Console.WriteLine("\n --- Now starting mutex multithread example ---");
        Thread.Sleep(1000);

        sw.Start();

        for (int i = 0; i < threadListMutex.Count; i++)
        {
            threadListMutex[i].Start();
            threadListMutex[i].Join();
        }

        sw.Stop();

        Console.WriteLine("Time elapsed for multithreading with Mutex: " + sw.Elapsed);
        Console.WriteLine("Count with mutex: " + MutexMethods.counter);
        Console.WriteLine("Count without mutex: " + MutexMethods.counter2);


        sw.Reset();
    }

    private static void SemaphoreThread()
    {
        semPool = new Semaphore(0,2);

        Console.WriteLine("\n --- Now starting semaphore multithread example ---");
        Thread.Sleep(1000);

        sw.Start();

        for (int i = 0; i < 10; i++)
        {
            threadListSemaphore[i].Start();
        }

        semPool.Release(2);

        for(int i = 0; i < 10; i++)
        {
            threadListSemaphore[i].Join();
        }

        sw.Stop();

        Console.ReadKey();
        sw.Reset();
    }

    private static void Setup()
    {
        if (threadList.Count > 0 && threadListMutex.Count > 0 && threadListSemaphore.Count > 0)
        {
            threadList.Clear();
            threadListMutex.Clear();
            threadListSemaphore.Clear();
        }

        MutexMethods.counter = 0;
        MutexMethods.counter2 = 0;
        // Initialises objects to be used in the thread example - Adds them to a list
        if (threadingObjectList.Count == 0)
        {
            for (int i = 0; i < 500; i++)
            {
                var myObject = new ThreadingObject("Name" + i, i, true, new List<KeyValuePair<int, string>>());
                threadingObjectList.Add(myObject);
            }
        }

        // Creates new thread processes referring to the multithread class methods and adds them to a list
        for (int i = 0; i < 50; i++)
        {
            threadList.Add(new Thread(() => ThreadMethods.Method1(threadingObjectList)));
            threadList.Add(new Thread(() => ThreadMethods.Method2(threadingObjectList)));

            threadListMutex.Add(new Thread(() => MutexMethods.Method1(threadingObjectList)));
            threadListMutex.Add(new Thread(() => MutexMethods.Method2(threadingObjectList)));
        }

        for (int i = 0; i < 10; i++)
        {
            threadListSemaphore.Add(new Thread((SemaphoreMethods.Method1)) { Name = i.ToString() });

        }
    }
}