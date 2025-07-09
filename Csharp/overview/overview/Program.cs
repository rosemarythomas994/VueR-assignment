using System;

namespace csharp_demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Basic Console Output
            Console.WriteLine("Hello World!");
            Console.Write("Hello World! ");
            Console.Write("I will print on the same line.");

            // Input
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();


            // Integer Input
            Console.Write("Enter your age: ");
            int age = Convert.ToInt32(Console.ReadLine());


            // Constant
            const int myNum = 15;
            Console.WriteLine("Constant number: " + myNum);
            // myNum = 20; // ❌ Error: Cannot assign to a const

            // Multiple Variable Declaration
            int r = 5, o = 6, s = 50;
            Console.WriteLine("Sum of r, o, s: " + (r + o + s));

           

            // Display Name and Age
            Console.WriteLine("Your Name is " + name);
            Console.WriteLine("Your age is " + age);
            age += 1;
            Console.WriteLine("Next year age is " + age);

            int myInt = 9;
            double myDouble = myInt;       // Automatic casting: int to double

            Console.WriteLine(myInt);      // Outputs 9
            Console.WriteLine(myDouble);   // Outputs 9


            double myDouble1 = 9.78;
            int myInt1 = (int)myDouble1;    // Manual casting: double to int

            Console.WriteLine(myDouble1);   // Outputs 9.78
            Console.WriteLine(myInt1);      // Outputs 9


            int myInt2 = 10;
            double myDouble2 = 5.25;
            bool myBool = true;

            Console.WriteLine(Convert.ToString(myInt2));    // convert int to string
            Console.WriteLine(Convert.ToDouble(myInt2));    // convert int to double
            Console.WriteLine(Convert.ToInt32(myDouble2));  // convert double to int
            Console.WriteLine(Convert.ToString(myBool));   // convert bool to string





            // String Operations
            string myString = "rose mary";
            string secondString = "But not that much";
            Console.WriteLine(myString + " " + secondString);
            Console.WriteLine("Length of myString: " + myString.Length);
            Console.WriteLine("Character at index 2: " + myString[2]);

            string firstName = "Rose";
            string lastName = "Joseph";
            string fullName = firstName + " " + lastName;
            Console.WriteLine("Full Name: " + fullName);
            Console.WriteLine($"Welcome, {firstName} {lastName}!"); // Interpolation

            // String Functions
            Console.WriteLine("UpperCase: " + firstName.ToUpper());
            Console.WriteLine("LowerCase: " + lastName.ToLower());
            Console.WriteLine("Contains 'Rose'? " + fullName.Contains("Rose"));
            Console.WriteLine("IndexOf 'Joseph': " + fullName.IndexOf("Joseph"));
            Console.WriteLine("Replace 'Rose' with 'Mary': " + fullName.Replace("Rose", "Mary"));
            Console.WriteLine("Substring (0–4): " + fullName.Substring(0, 4));

            // Numbers
            double myNumber = 32.54;
            Console.WriteLine("Fixed value: " + 30.25);
            Console.WriteLine("myNumber: " + myNumber);
            Console.WriteLine("Absolute of -17: " + Math.Abs(-17));

            // Integer Arithmetic
            int x = 10;
            int y = 4;
            Console.WriteLine("Sum: " + (x + y));
            Console.WriteLine("Difference: " + (x - y));
            Console.WriteLine("Product: " + (x * y));
            Console.WriteLine("Quotient: " + (x / y));      // Integer division
            Console.WriteLine("Remainder: " + (x % y));

            x++; // Increment
            y--; // Decrement
            Console.WriteLine($"After Increment/Decrement - x: {x}, y: {y}");

            // Double Arithmetic
            double a = 10.5;
            double b = 4.5;
            Console.WriteLine("Sum: " + (a + b));
            Console.WriteLine("Difference: " + (a - b));
            Console.WriteLine("Product: " + (a * b));
            Console.WriteLine("Quotient: " + (a / b));

            // Math Functions
            Console.WriteLine("Rounded a: " + Math.Round(a));
            Console.WriteLine("Ceiling b: " + Math.Ceiling(b));
            Console.WriteLine("Floor b: " + Math.Floor(b));
            Console.WriteLine("Max: " + Math.Max(a, b));
            Console.WriteLine("Min: " + Math.Min(a, b));

            // Boolean
            bool isLoggedIn = true;
            Console.WriteLine("Logged in? " + isLoggedIn);

            // Char
            char letter = 'R';
            Console.WriteLine("Letter: " + letter);

            // var Type Inference
            var autoString = "This is a string";
            var autoInt = 42;
            Console.WriteLine($"autoString: {autoString}, autoInt: {autoInt}");

            //String Interpolation
            string firstName1 = "Rose";
            string lastName1 = "mary";
            string nameFull = $"My full name is: {firstName1} {lastName1}";
            Console.WriteLine(nameFull);

            //expressions
            int ages = 25;
            Console.WriteLine($"Next year you will be{ages + 1} years old");

            double price = 15.56789;
            Console.WriteLine($"Price: {price:F3}"); //F3 3 decimal places


            DateTime now = DateTime.Now;
            Console.WriteLine($"Today is {now:dddd, MMMM dd yyyy}");

            //Access Strings
            string hello = "Hello";
            Console.WriteLine(hello[0]);
            Console.WriteLine(hello[1]);
            Console.WriteLine(hello[2]);
            Console.WriteLine(hello[3]);
            Console.WriteLine(myString.IndexOf("e"));

            string nameRose = "rose mary";
            int charPos = nameRose.IndexOf("e");
            
            if (charPos != -1)
            {
                string lastNameRose = nameRose.Substring(charPos);
                Console.WriteLine(lastNameRose);
            }
            else
            {
                Console.WriteLine("'m' not found in the string.");
            }


            // Pause before exit (for Windows console)
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
