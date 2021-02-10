using DatabaseManager;
using EmployeeManager;
using System;

namespace MainUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hello, would you like to add (1), modify (2) or delete (3) an existing employer? Just press enter to see database. ");
            string mainOption = Console.ReadLine();

            if (mainOption == "")
            {
                Console.WriteLine(Database.Output());
            }
            else
            {
                switch (mainOption)
                {
                    case "1":
                        Database.AddEmployee(CreateEmployee());
                        break;
                    case "2":
                        Database.EditEmployee();
                        break;
                    case "3":
                        Database.RemoveEmployee();
                        break;
                }
            }
        }

        static Employee CreateEmployee()
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
