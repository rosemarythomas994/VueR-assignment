using System;

namespace oops  // This is like a "package" (namespace)
{
    //  Interface
    interface IEngine
    {
        void StartEngine();
    }

    interface IInsurance
    {
        void ApplyInsurance(string provider);
    }

    //  Abstract class
    abstract class Vehicle
    {
        // Encapsulated private field
        private string brand;

        //  Property with get/set
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        // Automatic Property
        public int Year { get; set; }

        // Constructor
        public Vehicle(string brand, int year)
        {
            this.Brand = brand;
            this.Year = year;
        }

        //  Abstract method to override
        public abstract void ShowDetails();
    }

    // Car class - Inherits from Vehicle, implements 2 interfaces
    class Car : Vehicle, IEngine, IInsurance
    {
        private string model;

        public Car(string brand, int year, string model) : base(brand, year)
        {
            this.model = model;
        }

        public override void ShowDetails()
        {
            Console.WriteLine($"Car Brand: {Brand}, Year: {Year}, Model: {model}");
        }

        public void StartEngine()
        {
            Console.WriteLine("Car engine started!");
        }

        public void ApplyInsurance(string provider)
        {
            Console.WriteLine($"Insurance applied through: {provider}");
        }

        public void Honk()
        {
            Console.WriteLine("Car says: Beep beep!");
        }
    }

    //  Bike class - Inherits from Vehicle, implements 1 interface
    class Bike : Vehicle, IEngine
    {
        private string type;

        public Bike(string brand, int year, string type) : base(brand, year)
        {
            this.type = type;
        }

        public override void ShowDetails()
        {
            Console.WriteLine($"Bike Brand: {Brand}, Year: {Year}, Type: {type}");
        }

        public void StartEngine()
        {
            Console.WriteLine("Bike engine started!");
        }

        public void KickStart()
        {
            Console.WriteLine("Bike kick-started manually.");
        }
    }

    //  Main Program
    class Program
    {
        //  Enum definition
        enum WeekDay
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        static void Main(string[] args)
        {
            Console.WriteLine("🔹 Enum Example 🔹");

            WeekDay today = WeekDay.Monday;
            Console.WriteLine("Today is: " + today);            // Output: Monday
            Console.WriteLine("Numeric value: " + (int)today);  // Output: 0

            Console.WriteLine("\n🔹 OOP Examples 🔹");

            //  Create Car object
            Car myCar = new Car("Toyota", 2020, "Camry");
            myCar.ShowDetails();
            myCar.StartEngine();
            myCar.ApplyInsurance("LIC");
            myCar.Honk();

            Console.WriteLine();

            //  Create Bike object
            Bike myBike = new Bike("Royal Enfield", 2022, "Cruiser");
            myBike.ShowDetails();
            myBike.StartEngine();
            myBike.KickStart();

        }
    }
}
