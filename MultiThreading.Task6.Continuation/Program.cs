
Task parentTask = Task.Run(SimulateParentTask);

// Case 1: Continuation task should be executed regardless of the result of the parent task.
var continuation1 = parentTask.ContinueWith((antecedent) => ResultIndependentTask(), TaskContinuationOptions.None);

// Case 2: Continuation task should be executed when the parent task was completed without success.
Task continuation2 = parentTask.ContinueWith((antecedent) => ChildExecuteWithoutParentSuccess(), TaskContinuationOptions.OnlyOnFaulted);

//// Case 3: Continuation task should be executed when the parent task failed, and parent task thread should be reused for continuation.
Task continuation3 = parentTask.ContinueWith((antecedent) => WhenParentFault(), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

//// Case 4: Continuation task should be executed outside of the thread pool when the parent task is canceled.
CancellationTokenSource cts = new CancellationTokenSource();
Task parentTask4 = Task.Run(() => SimulateParentTaskWithCancellation(cts.Token), cts.Token);
Task.Run(() => { Thread.Sleep(500); cts.Cancel(); });

Task continuation4 = parentTask4.ContinueWith((antecedent) => Console.WriteLine("Continuation 4 executed"), TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.RunContinuationsAsynchronously);

// Wait for all tasks to complete.
Task.WaitAll(continuation1, continuation2, continuation3, continuation4);

// Commit for comments
Console.WriteLine("All tasks completed.");


void SimulateParentTask()
{
    Console.WriteLine("Parent task start sleep");
    Thread.Sleep(3000);
    Console.WriteLine("Parent task prepare throw exception");
    throw new Exception("Parent task failed.");
}

void ResultIndependentTask()
{
    Console.WriteLine("First continuation start sleep");
    Thread.Sleep(3000);
    Console.WriteLine("Continuation 1 executed");
}

void ChildExecuteWithoutParentSuccess()
{
    Console.WriteLine("Second continuation start sleep");
    Thread.Sleep(3000);
    Console.WriteLine("Continuation 2 executed");
}

void WhenParentFault()
{
    Console.WriteLine("Third continuation start sleep");
    Thread.Sleep(3000);
    Console.WriteLine("Continuation 3 executed");
}

void SimulateParentTaskWithCancellation(CancellationToken cancellationToken)
{
    Console.WriteLine("Task with cancellation token start sleep");
    Thread.Sleep(1000);
    Console.WriteLine("Task with cancellation token prepare throw exception");
    cancellationToken.ThrowIfCancellationRequested();
}





