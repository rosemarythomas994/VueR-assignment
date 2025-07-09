using System;

namespace MethodExamples
{
    class Program
    {
        // Non-static method (no parameters)
        void Greet()
        {
            Console.WriteLine("Hello from Greet method!");
        }

        //  Method with Parameters
        void SayHello(string name)
        {
            Console.WriteLine("Hello, " + name + "!");
        }

        // Method with Multiple Parameters
        void DisplayInfo(string name, int age)
        {
            Console.WriteLine($"{name} is {age} years old.");
        }

        // Default Parameter Value
        void Welcome(string name = "Guest")
        {
            Console.WriteLine($"Welcome, {name}!");
        }

        // Method with Return Value
        int Add(int a, int b)
        {
            return a + b;
        }

        //  Named Arguments
        void PrintOrder(string item, int quantity)
        {
            Console.WriteLine($"Order placed: {quantity} x {item}");
        }

        //Method Overloading
        void ShowMessage(string message)
        {
            Console.WriteLine("Message: " + message);
        }

        void ShowMessage(string message, int repeatCount)
        {
            for (int i = 0; i < repeatCount; i++)
            {
                Console.WriteLine("Message: " + message);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("🔹 Non-Static Method Demonstration 🔹\n");

            // Create an object of the class
            Program p = new Program();

            // Call method
            p.Greet();

            // With parameter
            p.SayHello("Rose");

            //  Multiple parameters
            p.DisplayInfo("John", 25);

            //  Default parameter
            p.Welcome();         // uses "Guest"
            p.Welcome("Mary");   // uses "Mary"

            //Method with return value
            int result = p.Add(10, 20);
            Console.WriteLine("Sum is: " + result);

            // Named arguments
            p.PrintOrder(quantity: 3, item: "Books");

            // Method overloading
            p.ShowMessage("Welcome!");
            p.ShowMessage("Repeat this", 2);

            
        }
    }
}