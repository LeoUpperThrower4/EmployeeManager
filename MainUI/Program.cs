using DatabaseManager;
using EmployeeManager;
using System;
using System.Collections.Generic;

namespace MainUI
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Hello, would you like to add (1), modify (2), delete (3) or search (4) an existing employee? Just press enter to see full database. ");
                string mainOption = Console.ReadLine();

                try
                {
                    switch (mainOption)
                    {
                        case "1":
                            AddEmployee();
                            break;
                        case "2":
                            ModifyEmployee();
                            break;
                        case "3":
                            RemoveEmployee();
                            break;
                        case "4":
                            SearchEmployee();
                            break;
                        default:
                            Console.WriteLine(Database.FullOutput());
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"\n*********\nEmployee not found.\n*********\n");
                }
                catch (Exception e)
                {
                    string message = "";
                    switch (mainOption)
                    {
                        case "1":
                            message = "add";
                            break;
                        case "2":
                            message = "modify";
                            break;
                        case "3":
                            message = "delete";
                            break;
                        case "4":
                            message = "find";
                            break;
                    }

                    Console.WriteLine($"Could not {message} employee. {e}");
                }

            }
        }

        private static void AddEmployee()
        {
            Console.WriteLine(Database.AddEmployee(AskEmployeeInfo()));
        }

        private static void ModifyEmployee()
        {
            Console.Write("Edit by ID (1) or by employee's info (2)? ");

            int option;
            while (!int.TryParse(Console.ReadLine().Replace(" ", ""), out option) && option != 1 && option != 2)
            {
                Console.WriteLine("Please type only integer.");
            }

            if (option == 1)
            {
                Console.Write("What is the employee's ID? ");
                string id = Console.ReadLine();
                Console.WriteLine($"\nSelected employee's ID: { Database.SearchEmployee(id)[0] }\n");

                Console.WriteLine("********New employee********");
                IEmployee newEmployee = AskEmployeeInfo();
                Database.RemoveEmployee(id);
                Database.AddEmployee(newEmployee);
            }
            else if (option == 2)
            {

                IEmployee firstEmployee = AskEmployeeInfo();
                Console.WriteLine($"\nSelected employee's ID: { Database.SearchEmployee(firstEmployee)[0] }\n");

                Console.WriteLine("********New employee********");
                IEmployee newEmployee = AskEmployeeInfo();
                Database.RemoveEmployee(firstEmployee);
                Database.AddEmployee(newEmployee);
            }
            else
            {
                Console.WriteLine("Type only 1 or 2? ");
            }


            Console.WriteLine("Employee successfully modified.");

        }

        private static void RemoveEmployee()
        {
            Console.Write("Remove by ID (1) or by employee's info (2)? ");

            int option;

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
        }

        private static void SearchEmployee()
        {
            Console.Write("Search by ID (1) or by employee's info (2)? ");

            int option = 0;

            do
            {
                while (!int.TryParse(Console.ReadLine().Replace(" ", ""), out option))
                {
                    Console.WriteLine("Please type only integer.");
                }

                List<IEmployee> employees;

                if (option == 1)
                {
                    Console.Write("What is the employee's ID? ");
                    string id = Console.ReadLine();
                    employees = Database.SearchEmployee(id);

                    if (employees.Count == 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    else
                    {
                        employees.ForEach(e => Console.WriteLine(e.ToString()));
                    }
                }
                else if (option == 2)
                {
                    employees = Database.SearchEmployee(AskEmployeeInfo());

                    if (employees.Count == 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    else
                    {
                        employees.ForEach(e => Console.WriteLine(e.ToString()));
                    }
                }
                else
                {
                    Console.WriteLine("Type only 1 or 2. ");
                }
            } while (option != 1 && option != 2);
        }

        static IEmployee AskEmployeeInfo()
        {
            IEmployee e;
            Console.Write("\nWhat's the employee's first name? ");
            string firstName = Console.ReadLine().Replace(" ", "");

            Console.Write("What's the employee's last name? ");
            string lastName = Console.ReadLine().Replace(" ", "");

            int age;

            Console.Write("What's the employee's age? ");

            while (!int.TryParse(Console.ReadLine().Replace(" ", ""), out age))
            {
                Console.WriteLine("Please type only integer.");
            }

            Console.Write("What's the employee's type? (0) Physical (1) Remote. ");

            int type;

            while (!int.TryParse(Console.ReadLine().Replace(" ", ""), out type) && type != 1 && type != 2)
            {
                Console.WriteLine("Please type only integer. (0) Physical (1) Remote. ");
            }

            if (type == 0)
            {
                e = new PhysicalEmployee { FirstName = firstName, Age = age, LastName = lastName };
            }
            else
            {
                e = new RemoteEmployee { FirstName = firstName, Age = age, LastName = lastName };
            }


            return e;

        }
    }
}
