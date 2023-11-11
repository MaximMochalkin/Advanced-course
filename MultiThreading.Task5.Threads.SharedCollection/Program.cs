using System.Collections.Concurrent;

//var sharedCollection = new ConcurrentQueue<int>();
var sharedCollection = new List<int>();
AutoResetEvent addSignal = new AutoResetEvent(false);
AutoResetEvent printSignal = new AutoResetEvent(true);

var lockObject = new object();

var addingThread = new Thread(AddElementsToCollection);
var printingThread = new Thread(PrintElementsFromCollection);

addingThread.Start();
printingThread.Start();

addingThread.Join();
printingThread.Join();

Console.ReadLine();

void AddElementsToCollection()
{
    for (var i = 1; i <= 10; i++)
    {
        lock (sharedCollection)
        {
            sharedCollection.Add(i);
            Console.WriteLine($"Added {i} to the collection.");
        }
        addSignal.Set(); // Notify the printing thread
        if (i <= 10)
            printSignal.WaitOne(); // Wait for the printing thread to print
    }
}

void PrintElementsFromCollection()
{
    while (true)
    {
        addSignal.WaitOne(); // Wait for the adding thread to add elements
        lock (sharedCollection)
        {
            int count = sharedCollection.Count;
            List<int> sublist = sharedCollection.GetRange(0, count);
            Console.WriteLine($"[{string.Join(", ", sublist)}]");
        }
        if (sharedCollection.Count == 10)
            break; // All elements added
        printSignal.Set(); // Notify the adding thread
    }
}