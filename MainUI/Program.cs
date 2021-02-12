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
                Console.Write("Hello, would you like to add (1), modify (2), delete (3) or search (4) an existing employer? Just press enter to see full database. ");
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
            Database.EditEmployee();
        }

        private static void RemoveEmployee()
        {
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

                if (option == 1)
                {
                    Console.Write("What is the employee's ID? ");
                    string id = Console.ReadLine();
                    try
                    {
                        Console.WriteLine(Database.SearchEmployee(id));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine($"\n*********\nEmployee not found.\n*********\n");
                        continue;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e}");
                    }
                }
                else if (option == 2)
                {
                    try
                    {
                        Console.WriteLine(Database.SearchEmployee(AskEmployeeInfo()).ToString());
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // handled later on the stack
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e}");
                    }
                }
                else
                {
                    Console.WriteLine("Type only 1 or 2? ");
                }
            } while (option != 1 && option != 2);
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
