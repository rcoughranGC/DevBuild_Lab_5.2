using System;
using System.Collections.Generic;
using System.Linq;

namespace DevBuild_Lab5_2
{
    enum CarMake
    {
        Ford,
        Chevrolet,
        Nissan,
        Ferrari,
        AstonMartin
    }

    class Car
    {
        protected CarMake _make;
        protected string _model;
        protected int _year;
        protected decimal _price;
        public int GetYear()
        {
            return _year;
        }
        public Car (CarMake make, string model, int year, decimal price)
        {
            _make = make;
            _model = model;
            _year = year;
            _price = price;
        }
    }
    class NewCar : Car
    {
        protected bool _extendedWarranty;
        public NewCar(CarMake make, string model, int year, decimal price, bool extendedWarranty) : base (make, model, year, price)
        {
            _extendedWarranty = extendedWarranty;
        }
        public override string ToString()
        {
            string warranty;
            if (_extendedWarranty)
            {
                warranty = "Yes";
            }
            else
            {
                warranty = "No";
            }
            return $"{_year}\t {_make} {_model,-10}\t  {_price:C0}\tMileage: N/A \t (Ext Warranty?: {warranty})";
        }
    }
    class UsedCar : Car
    {
        protected int _numberOfOwners;
        protected int _mileage;
        public UsedCar(CarMake make, string model, int year, decimal price, int numberOfOwners, int mileage) : base (make, model, year, price)
        {
            _numberOfOwners = numberOfOwners;
            _mileage = mileage;
        }
        public override string ToString()
        {
            return $"{_year}\t {_make} {_model,-10}\t  {_price:C0}\tMileage: {_mileage}\t (Previous Owners: {_numberOfOwners})";  //messy code but nicer console output.
        }
    }

    class Program
    {
        static void Menu(List<Car> cars)       // Method to display the cars and the menu options.
        {
            Console.WriteLine("Current Vehicle Inventory:\n");
            int carNumber = 1;
            foreach (Car listing in cars)
            {
                Console.WriteLine($"{carNumber}. {listing}");
                carNumber++;
            }
            Console.WriteLine($"\nSelect a car 1 - {carNumber-1}:");
            Console.WriteLine("A. Add a car to inventory");
            Console.WriteLine("Q. Quit");
        }
        static void SortCars(List<Car> cars)   //Added a built-in sort to have some sort of order to the list. Sorted by year.
        {
            Car temp;
            for (int i = 0; i < cars.Count; i++)
            {
                for (int j = 0; j < cars.Count -1; j++)
                {
                    if (cars[j].GetYear() > cars[j + 1].GetYear())
                    {
                        temp = cars[j + 1];
                        cars[j + 1] = cars[j];
                        cars[j] = temp;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            // Populate initial data
            List<Car> carInventory = new List<Car>();
            carInventory.Add(new UsedCar(CarMake.Ford, "Mustang", 2001, 3999M, 3, 141289));
            carInventory.Add(new UsedCar(CarMake.Chevrolet, "Corvette", 2011,17999, 1, 78000));
            carInventory.Add(new UsedCar(CarMake.AstonMartin, "DB9", 2008, 39995, 1, 68000));
            carInventory.Add(new NewCar(CarMake.Ferrari, "F8", 2021, 306450, false));
            carInventory.Add(new NewCar(CarMake.Nissan, "GT-R", 2021, 113540, true));
            carInventory.Add(new NewCar(CarMake.Ford, "GT", 2021, 500000, true));
            carInventory.Add(new UsedCar(CarMake.Nissan, "Maxima", 2012, 11000, 3, 168000));

            // Start the program
            while (true)
            {
                SortCars(carInventory);
                Menu(carInventory);

                string userInput = Console.ReadLine();
                if (userInput.ToLower() == "q")
                {
                    Console.WriteLine("\nGoodbye!");
                    break;
                }
                else if (userInput.ToLower() == "a")
                {
                    Console.Write("Enter New or Used: ");
                    userInput = Console.ReadLine();

                    if (userInput.ToLower() == "new")                 // Add a new car
                    {
                        Console.Write("Enter Ford, Chevrolet, Nissan, Ferrari, or AstonMartin: ");
                        userInput = Console.ReadLine();
                        Enum.TryParse(userInput, out CarMake make);   //No idea how to error check this. Did not try. If you add A Dodge Ram it becomes a Ford Ram.
                        Console.Write("Enter the Model: ");
                        string model = Console.ReadLine();
                        Console.Write("Enter the year: ");
                        int year = int.Parse(Console.ReadLine());
                        Console.Write("Enter the Price: ");
                        int price = int.Parse(Console.ReadLine());
                        bool exWarranty;
                        Console.Write("Is there an extended warranty? (y/n): ");
                        userInput = Console.ReadLine();
                        if (userInput == "y")
                        {
                            exWarranty = true;
                        }
                        else
                        {
                            exWarranty = false;
                        }
                        carInventory.Add(new NewCar(make, model, year, price, exWarranty));
                        Console.WriteLine();

                    }
                    else if (userInput.ToLower() == "used")         // Add a used car
                    {
                        Console.Write("Enter Ford, Chevrolet, Nissan, Ferrari, or AstonMartin: ");
                        userInput = Console.ReadLine();
                        Enum.TryParse(userInput, out CarMake make);
                        Console.Write("Enter the Model: ");
                        string model = Console.ReadLine();
                        Console.Write("Enter the year: ");
                        int year = int.Parse(Console.ReadLine());
                        Console.Write("Enter the Price: ");
                        int price = int.Parse(Console.ReadLine());
                        Console.Write("Enter the mileage: ");
                        int mileage = int.Parse(Console.ReadLine());
                        Console.Write("How many previous owners?: ");
                        int prevOwners = int.Parse(Console.ReadLine());
                        carInventory.Add(new UsedCar(make, model, year, price, prevOwners, mileage));
                        Console.WriteLine();
                    }
                }
                else if (userInput.ToLower() != "a" && userInput.ToLower() != "q")    // If user doesn't pick the Add or Quit option
                {                                                                     // it attempts to select a car
                    int.TryParse(userInput, out int carSelection);                 
                                                                                   
                    if (carSelection <= carInventory.Count && carSelection > 0)       // If a number outside the range, or a letter besides Q or A is entered
                    {                                                                 // the menu just reloads.
                        carSelection = int.Parse(userInput) - 1;                      // Line up the user's selection with the indexing of the car list
                        Console.WriteLine($"You have selected: {carInventory[carSelection]}");
                        Console.WriteLine("Purchase? (y/n): ");
                        if (Console.ReadLine() == "y")
                        {
                            carInventory.Remove(carInventory[carSelection]);          // Sell and remove the car
                        }
                    }
                }
            }
        }
    }
}
