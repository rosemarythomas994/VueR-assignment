using System;

namespace LoopsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            if (20 > 18)
            {
                Console.WriteLine("20 is greater than 18");
            }
            Console.WriteLine("Enter your age: ");
            int age = Convert.ToInt32(Console.ReadLine());
            // if
            if (age >= 18)
            {
                Console.WriteLine("You are an adult.");
            }

            // if...else
            if (age >= 60)
            {
                Console.WriteLine("You are a senior citizen.");
            }
            else
            {
                Console.WriteLine("You are not a senior citizen.");
            }

            // if...else if...else
            if (age < 13)
            {
                Console.WriteLine("You are a child.");
            }
            else if (age < 20)
            {
                Console.WriteLine("You are a teenager.");
            }
            else if (age < 60)
            {
                Console.WriteLine("You are an adult.");
            }
            else
            {
                Console.WriteLine("You are a senior.");
            }

            // Short Hand if...else (Ternary)
            string canVote = (age >= 18) ? "Yes, you can vote!" : "No, you're too young to vote.";
            Console.WriteLine("Voting eligibility: " + canVote);

        }
    }
}
