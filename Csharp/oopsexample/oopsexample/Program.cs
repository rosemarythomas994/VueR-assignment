using System;

namespace VehicleApp  //  This is like a "package" (namespace)
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

    // Abstract class
    abstract class Vehicle
    {
        //  Encapsulated field
        private string brand;

        // Property with get/set
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        //  Automatic Property
        public int Year { get; set; }

        //  Constructor
        public Vehicle(string brand, int year)
        {
            this.Brand = brand;
            this.Year = year;
        }

        // Method to override
        public abstract void ShowDetails();
    }

    //  Inheritance & Interface Implementation
    class Car : Vehicle, IEngine, IInsurance
    {
        //  Field
        private string model;

        // Constructor with parameter
        public Car(string brand, int year, string model) : base(brand, year)
        {
            this.model = model;
        }

        //  Overriding abstract method
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

        // Object Method
        public void Honk()
        {
            Console.WriteLine("Car says: Beep beep!");
        }
    }

    //  Another class
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
        static void Main(string[] args)
        {


            //oops examples

            //  Creating multiple objects
            Car myCar = new Car("Toyota", 2020, "Camry");
            myCar.ShowDetails();
            myCar.StartEngine();
            myCar.ApplyInsurance("LIC");
            myCar.Honk();

            Console.WriteLine();

            Bike myBike = new Bike("Royal Enfield", 2022, "Cruiser");
            myBike.ShowDetails();
            myBike.StartEngine();
            myBike.KickStart();

        }
    }
}
