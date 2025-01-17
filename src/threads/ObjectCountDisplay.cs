﻿using OOD_24L_01180689.src.dataStorage;

public class ObjectCountDisplay
{
    private static ObjectCountDisplay instance;
    private static readonly object instanceLock = new();
    private readonly Thread countDisplayThread;
    private bool stay = true;

    private ObjectCountDisplay()
    {
        countDisplayThread = new Thread(() =>
        {
            try
            {
                DisplayObjectCount();
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine("Object count interrupted");
            }
        })
        {
            IsBackground = true
        };
    }

    public static ObjectCountDisplay GetInstance
    {
        get
        {
            if (instance == null)
                lock (instanceLock)
                {
                    if (instance == null) instance = new ObjectCountDisplay();
                }

            return instance;
        }
    }

    private void DisplayObjectCount()
    {
        while (stay)
        {
            var objectListCount = DataStorage.GetInstance.CountObjectList();
            lock (instanceLock)
            {
                var previousLeft = Console.CursorLeft;
                var previousTop = Console.CursorTop;
                Console.SetCursorPosition(0, 0);
                Console.Write($"Current number of objects: {objectListCount}\n");
                Console.SetCursorPosition(previousLeft, previousTop);
            }

            Thread.Sleep(500);
        }
    }

    public void Start()
    {
        if (!countDisplayThread.IsAlive) countDisplayThread.Start();
    }

    public void Stop()
    {
        stay = false;
        if (countDisplayThread != null && countDisplayThread.IsAlive)
        {
            countDisplayThread.Interrupt();
            countDisplayThread.Join();
        }
    }
}