using DatabaseManager;
using EmployeeManager;
using System;

namespace MainUI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Hello, would you like to add (1), modify (2) or delete (3) an existing employer? Just press enter to see database. ");
                string mainOption = Console.ReadLine();

                switch (mainOption)
                {
                    case "1":
                        Database.AddEmployee(AskEmployeeInfo());
                        break;
                    case "2":
                        Database.EditEmployee();
                        break;
                    case "3":
                        Console.WriteLine(Database.RemoveEmployee(AskEmployeeInfo()));
                        break;
                    default:
                        Console.WriteLine(Database.Output());
                        break;
                }

            }
        }

        static Employee AskEmployeeInfo()
        {
            Employee e = new Employee();
            Console.Write("What's the employee's first name? ");
            e.FirstName = Console.ReadLine();
            Console.Write("What's the employee's last name? ");
            e.LastName = Console.ReadLine();
            Console.Write("What's the employee's age? ");

            int age = 0;

            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Please type only integer.");
            }

            e.Age = age;

            return e;
        }
    }
}
