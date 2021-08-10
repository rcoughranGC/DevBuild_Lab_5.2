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
            return $"{_year}\t {_make} {_model,-10}\t  {_price:C0}\t(Ext Warranty?: {warranty})";
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
            return $"{_year}\t {_make} {_model,-10}\t  {_price:C0}\t({_mileage}mi - Previous Owners: {_numberOfOwners})";
        }
    }

    class Program
    {
        static void Menu(List<Car> cars)
        {
            Console.WriteLine("Select a car:");
            int menuNumber = 1;
            foreach (Car listing in cars)
            {
                Console.WriteLine($"{menuNumber}. {listing}");
                menuNumber++;
            }
            Console.WriteLine("\nA. Add a car to inventory");
            Console.WriteLine("Q. Quit");
        }
        //static void SortCars(List<Car> cars)
        //{
        //    Car temp;
        //    for (int i = 0; i < cars.Count-1; i++)
        //    {
        //        for (int j = 0; j < cars.Count; j++)
        //        {
        //            if (cars[i].GetYear() > cars[j].GetYear())
        //            {
        //                temp = cars[i + 1];
        //                cars[i + 1] = cars[i];
        //                cars[i] = temp;

        //            }
        //        }
        //    }
        //}
        static void Main(string[] args)
        {
            List<Car> carInventory = new List<Car>();
            carInventory.Add(new UsedCar(CarMake.Ford, "Mustang", 2001, 3999M, 3, 141289));
            carInventory.Add(new UsedCar(CarMake.Chevrolet, "Corvette", 2011,17999, 1, 78000));
            carInventory.Add(new UsedCar(CarMake.AstonMartin, "DB9", 2008, 39995, 1, 68000));
            carInventory.Add(new NewCar(CarMake.Ferrari, "F8", 2021, 306450, false));
            carInventory.Add(new NewCar(CarMake.Nissan, "GT-R", 2021, 113540, true));
            carInventory.Add(new NewCar(CarMake.Ford, "GT", 2021, 500000, true));
            carInventory.Add(new UsedCar(CarMake.Nissan, "Maxima", 2012, 11000, 3, 168000));


            while (true)
            {
                //SortCars(carInventory);
                
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
                    if (userInput.ToLower() == "new")
                    {
                        Console.Write("Enter Ford, Chevrolet, Nissan, Ferrari, or AstonMartin: ");
                        userInput = Console.ReadLine();
                        Enum.TryParse(userInput, out CarMake newCarMake);
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
                        carInventory.Add(new NewCar(newCarMake, model, year, price, exWarranty));
                        Console.WriteLine();

                    }
                    else if (userInput.ToLower() == "used")
                    {
                        Console.Write("Enter Ford, Chevrolet, Nissan, Ferrari, or AstonMartin: ");
                        userInput = Console.ReadLine();
                        Enum.TryParse(userInput, out CarMake usedCarMake);
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
                        carInventory.Add(new UsedCar(usedCarMake, model, year, price, prevOwners, mileage));
                        Console.WriteLine();

                    }
                }
                else if (userInput.ToLower() != "a" && userInput.ToLower() != "q")
                {   
                    int.TryParse(userInput, out int carSelection);
                
                    if (carSelection <= carInventory.Count && carSelection > 0)
                    {
                        carSelection = int.Parse(userInput) - 1;
                        Console.WriteLine($"You have selected {carInventory[carSelection]}");
                        Console.WriteLine("Purchase? (y/n): ");
                        if (Console.ReadLine() == "y")
                        {
                            carInventory.Remove(carInventory[carSelection]);
                        }
                    }
                }
            }
        }
    }
}
