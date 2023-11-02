const int TaskAmount = 100;
const int MaxIterationsCount = 1000;

Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
Console.WriteLine("“Task #0 – {iteration number}”.");
Console.WriteLine();

HundredTasks();

Console.ReadLine();

static void HundredTasks()
{
    Task[] tasks = new Task[TaskAmount];

    for (int taskNumber = 0; taskNumber < TaskAmount; taskNumber++)
    {
        tasks[taskNumber] = Task.Run(() =>
        {
            for (int iterationNumber = 1; iterationNumber <= MaxIterationsCount; iterationNumber++)
            {
                Output(taskNumber, iterationNumber);
            }
        });
    }

    Task.WaitAll(tasks);
}

static void Output(int taskNumber, int iterationNumber)
{
    Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
}