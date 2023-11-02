Task<int[]> firstTask = Task.Run(() =>
{
    Random random = new Random();
    int[] randomArray = Enumerable.Range(0, 10).Select(_ => random.Next(1, 101)).ToArray();
    Console.WriteLine("Generated Array: " + string.Join(", ", randomArray));
    return randomArray;
});

Task<int[]> secondTask = firstTask.ContinueWith(prevTask =>
{
    Random random = new Random();
    int randomMultiplier = random.Next(1, 11);
    int[] multipliedArray = prevTask.Result.Select(x => x * randomMultiplier).ToArray();
    Console.WriteLine("Multiplied Array: " + string.Join(", ", multipliedArray));
    return multipliedArray;
});

Task<int[]> thirdTask = secondTask.ContinueWith(prevTask =>
{
    int[] sortedArray = prevTask.Result.OrderBy(x => x).ToArray();
    Console.WriteLine("Sorted Array: " + string.Join(", ", sortedArray));
    return sortedArray;
});

Task<double> fourthTask = thirdTask.ContinueWith(prevTask =>
{
    double average = prevTask.Result.Average();
    Console.WriteLine("Average Value: " + average);
    return average;
});

fourthTask.Wait();