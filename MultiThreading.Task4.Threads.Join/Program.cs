using MultiThreading.Task4.Threads.Join;


var semaphoreHelper = new SemaphoreHelper(0, 10);

Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
Console.WriteLine("Implement all of the following options:");
Console.WriteLine();
Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

Console.WriteLine();
semaphoreHelper.Run();
Console.ReadLine();