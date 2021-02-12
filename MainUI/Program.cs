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
                        Console.WriteLine(Database.AddEmployee(AskEmployeeInfo()));
                        Database.SaveDatabase();
                        break;
                    case "2":
                        Database.EditEmployee();
                        break;
                    case "3":
                        Console.Write("Remove by ID (1) or by employee's info (2)? ");

                        int option = 0;

                        do
                        {
                            while (!int.TryParse(Console.ReadLine().Replace(" ", ""), out option))
                            {
                                Console.WriteLine("Please type only integer.");
                            }

                            if (option == 1)
                            {
                                Console.Write("What is the employee's ID? ");
                                string id = Console.ReadLine();
                                Console.WriteLine(Database.RemoveEmployee(id));
                            }
                            else if (option == 2)
                            {
                                Console.WriteLine(Database.RemoveEmployee(AskEmployeeInfo()));
                            }
                            else
                            {
                                Console.WriteLine("Type only 1 or 2? ");
                            }
                        } while (option != 1 && option != 2);
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
            Console.Write("\nWhat's the employee's first name? ");
            e.FirstName = Console.ReadLine().Replace(" ", "");
            Console.Write("What's the employee's last name? ");
            e.LastName = Console.ReadLine().Replace(" ", "");
            Console.Write("What's the employee's age? ");

            int age = 0;

            while (!int.TryParse(Console.ReadLine().Replace(" ", ""), out age))
            {
                Console.WriteLine("Please type only integer.");
            }

            e.Age = age;

            return e;
        }
    }
}
