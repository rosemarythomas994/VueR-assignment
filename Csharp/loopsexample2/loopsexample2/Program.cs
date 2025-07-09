using System;

namespace LoopsExmaples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Switch Statement
            Console.Write("Enter a number (1-3): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("You chose One.");
                    break;
                case 2:
                    Console.WriteLine("You chose Two.");
                    break;
                case 3:
                    Console.WriteLine("You chose Three.");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("\n While Loop:");

            //while loop example
            int i = 0;
            while (i < 3)
            {
                Console.WriteLine("Count: " + i);
                i++;
            }
            //do while loop example
            Console.WriteLine("\n Do/While Loop:");
            int j = 0;
            do
            {
                Console.WriteLine("Do loop count: " + j);
                j++;
            } while (j < 3);

            //for loop example
            Console.WriteLine("\n For Loop:");
            for (int k = 1; k <= 5; k++)
            {
                if (k == 3) continue; // Skip 3
                if (k == 5) break;    // Stop at 5
                Console.WriteLine("Number: " + k);
            }

            //foreach example
            Console.WriteLine("\n Foreach Loop:");
            string[] colors = { "Red", "Green", "Blue" };
            foreach (string color in colors)
            {
                Console.WriteLine(color);
            }

            // Arrays
            Console.WriteLine("\n Arrays - Change and Access:");
            int[] numbers = { 10, 20, 30 };
            numbers[1] = 25;  // Change second element
            Console.WriteLine("Modified value: " + numbers[1]);

            Console.WriteLine("\n Loop Through Array (for loop):");
            for (int a = 0; a < numbers.Length; a++)
            {
                Console.WriteLine("Index " + a + ": " + numbers[a]);
            }

            Console.WriteLine("\n Sort Array:");
            int[] unsorted = { 40, 10, 30, 20 };
            Array.Sort(unsorted);

            Console.WriteLine("Sorted values:");
            foreach (int val in unsorted)
            {
                Console.WriteLine(val);
            }

            Console.WriteLine("\n Multidimensional Array:");
            int[,] matrix = {
                {1, 2},
                {3, 4},
                {5, 6}
            };

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n Program completed!");
        }
    }
}
