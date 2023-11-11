using AsyncAwait.Task1.CancellationTokens;

Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
Console.WriteLine("Calculating the sum of integers from 0 to N.");
Console.WriteLine("Use 'q' key to exit...");
Console.WriteLine();

Console.WriteLine("Enter N: ");

var input = Console.ReadLine();
var cts = new CancellationTokenSource();
while (input?.Trim().ToUpper() != "Q")
{
    try
    {
        if (int.TryParse(input, out var n) && n > 0)
        {
            Console.WriteLine($"The task for {n} started... Enter N to cancel the request or 'c' to continue:");
            var newInput = Console.ReadLine();

            if (int.TryParse(newInput, out var newN) && newN > 0)
            {
                cts.Cancel();
                await CalculateSum(newN, cts, true).ConfigureAwait(false);
            }

            if (!cts.Token.IsCancellationRequested)
            {
                await CalculateSum(n, cts).ConfigureAwait(false);
            }
        }
        else
        {
            Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
            Console.WriteLine("Enter N: ");
        }

        input = Console.ReadLine();
    }
    catch (OperationCanceledException e)
    {
        Console.WriteLine("Calculation aborted. Enter a new N to restart or press Enter to exit.");
    }
}

Console.WriteLine("Press any key to continue");
Console.ReadLine();

static async Task CalculateSum(int n, CancellationTokenSource cts, bool isNewValue = false)
{
    if (!isNewValue)
    {
        var result = await Calculator.Calculate(n, cts.Token).ConfigureAwait(false);
        Console.WriteLine($"Sum for {n} = {result}.");
        Console.WriteLine();
        Console.WriteLine("Enter N: ");
    }
    else
    {
        if (cts.Token.IsCancellationRequested)
        {
            var newCts = new CancellationTokenSource();
            var result = await Calculator.Calculate(n, newCts.Token).ConfigureAwait(false);
            Console.WriteLine($"Sum for {n} = {result}.");
            Console.WriteLine();
            Console.WriteLine("Enter N: ");
        }
    }
    
}