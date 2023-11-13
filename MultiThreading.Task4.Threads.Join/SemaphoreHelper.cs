namespace MultiThreading.Task4.Threads.Join
{
    public class SemaphoreHelper
    {
        public static Semaphore semaphore;

        public SemaphoreHelper(int initialCount, int maximumCount)
        {
            semaphore = new Semaphore(initialCount, maximumCount);
        }

        public void Run()
        {
            Console.WriteLine();

            var initialState = 10;

            Console.WriteLine("Using Thread class and Join:");
            Thread thread = new Thread(() => RunThreadUsingThreadClass(initialState));
            thread.Start();
            thread.Join();

            Console.WriteLine("\nUsing ThreadPool class and Semaphore:");
            ThreadPool.QueueUserWorkItem(RunThreadUsingThreadPool, initialState);
            semaphore.WaitOne();
        }

        private static void RunThreadUsingThreadClass(int state)
        {
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}, State: {state}");
            if (state > 0)
            {
                Thread thread = new Thread(() => RunThreadUsingThreadClass(state - 1));
                thread.Start();
                thread.Join();
            }
        }

        private static void RunThreadUsingThreadPool(object stateObj)
        {
            var state = (int)stateObj;
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}, State: {state}");
            if (state > 0)
            {
                ThreadPool.QueueUserWorkItem(RunThreadUsingThreadPool, state - 1);
            }
            else
            {
                semaphore.Release(); // Signal that this thread is done
            }
        }
    }
}
