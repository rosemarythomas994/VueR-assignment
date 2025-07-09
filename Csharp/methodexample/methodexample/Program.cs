using System;

namespace MethodExamples
{
    class Program
    {
        // Create a Method (void, no parameters)
        static void Greet()
        {
            Console.WriteLine("Hello from Greet method!");
        }

        // Method with Parameters
        static void SayHello(string name)
        {
            Console.WriteLine("Hello, " + name + "!");
        }

        // Method with Multiple Parameters
        static void DisplayInfo(string name, int age)
        {
            Console.WriteLine($"{name} is {age} years old.");
        }

        // Default Parameter Value
        static void Welcome(string name = "Guest")
        {
            Console.WriteLine($"Welcome, {name}!");
        }

        //Method with Return Value
        static int Add(int a, int b)
        {
            return a + b;
        }

        // Named Arguments
        static void PrintOrder(string item, int quantity)
        {
            Console.WriteLine($"Order placed: {quantity} x {item}");
        }

        // Method Overloading (same name, different parameters)
        static void ShowMessage(string message)
        {
            Console.WriteLine("Message: " + message);
        }

        static void ShowMessage(string message, int repeatCount)
        {
            for (int i = 0; i < repeatCount; i++)
            {
                Console.WriteLine("Message: " + message);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("🔹 Method Demonstration Starts 🔹\n");

            //  Call a Method
            Greet();

            // Pass a parameter
            SayHello("Rose");

            // Multiple Parameters
            DisplayInfo("Joseph", 25);

            //  Default Parameter Value
            Welcome();             // Uses default
            Welcome("Mary");       // Uses passed value

            // Method with Return Value
            int sum = Add(10, 20);
            Console.WriteLine($"Sum is: {sum}");

            // Named Arguments (pass in any order)
            PrintOrder(quantity: 3, item: "Books");

            // Method Overloading
            ShowMessage("Welcome!");
            ShowMessage("Repeat this!", 2);

        }
    }
}
