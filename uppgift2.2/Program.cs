// Uppgift 2.2
// Programmering i C#.NET DT071G -- HT2025
// Maamoun Okla , maok1900@student.miun.se

 
using System;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        // kontrollera argument
        if (args.Length < 2 || args[0] != "-p")
        {
            Console.WriteLine("Usage: dotnet run -- -p 0  (0 = ascending, 1= descending)");
            return;
        }

        bool ascending = args[1] == "0";

        // skapa och fyll i array:
        int[] arr = new int[1000];
        Random r = new Random();

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = r.Next(0, 1000);
        }

        // En klon av arr för Array.Sort metoden 
        int[] arrCopy = (int[])arr.Clone();

        //Skriv ut den osorterade arrayen
        Console.WriteLine("Unsorted Array: ");
        Console.WriteLine(string.Join(",", arr));

        //Bubbel Sortering

        Stopwatch stop1 = Stopwatch.StartNew();
        BubbleSort(arr, ascending);
        stop1.Stop();

        long BubbleSortMilliSec = stop1.ElapsedMilliseconds;


        //Array.Sort
        Stopwatch stop2 = Stopwatch.StartNew();
        Array.Sort(arrCopy);

        if (!ascending)
        {
            Array.Reverse(arrCopy);

        }
        stop2.Stop();

        long arraySortMilliSec = stop2.ElapsedMilliseconds;

        //Skriv ut resultatet: 
        Console.WriteLine("BubbleSort: ");
        Console.WriteLine(string.Join(", ", arr));

        Console.WriteLine("ArraySort: ");
        Console.WriteLine(string.Join(", ", arrCopy));

        //Tid för varje sorteringsmetod: 
        Console.WriteLine($"Tid för BubbleSort: {BubbleSortMilliSec} ms");
        Console.WriteLine($"Tid för Array.Sort: {arraySortMilliSec} ms");



    }
    //*****Metoder****//

    //Bubble Sort Metod
    static void BubbleSort(int[] arr, bool ascending)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1; j++)
            {
                bool Swap =
                (ascending && arr[j] > arr[j + 1]) || (!ascending && arr[j] < arr[j + 1]);

                if (Swap)
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;

                }
            }
        }
    }
}
