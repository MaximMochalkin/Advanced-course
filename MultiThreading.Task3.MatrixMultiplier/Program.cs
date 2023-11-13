using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;


Console.WriteLine("3.	Write a program, which multiplies two matrices and uses class Parallel. ");
Console.WriteLine();

const byte matrixSize = 7; // todo: use any number you like or enter from console
CreateAndProcessMatrices(matrixSize);
Console.ReadLine();


static void CreateAndProcessMatrices(byte sizeOfMatrix)
{
    Console.WriteLine("Multiplying...");
    var firstMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix);
    var secondMatrix = new Matrix(sizeOfMatrix, sizeOfMatrix);

    IMatrix resultMatrix = new MatricesMultiplier().Multiply(firstMatrix, secondMatrix);

    var parallelResultMatrix = new MatricesMultiplierParallel().Multiply(firstMatrix, secondMatrix);


    Console.WriteLine("firstMatrix:");
    firstMatrix.Print();
    Console.WriteLine("secondMatrix:");
    secondMatrix.Print();
    Console.WriteLine("resultMatrix:");
    resultMatrix.Print();
    Console.WriteLine("parallelResultMatrix: ");
    parallelResultMatrix.Print();
}