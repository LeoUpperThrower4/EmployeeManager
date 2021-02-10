﻿using EmployeeManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseManager
{
    public static class Database
    {

        static Database()
        {
            CreateSampleData();
        }

        private static List<Employee> employees = new List<Employee>();

        private static void CreateSampleData()
        {
            employees.Add(new Employee("Leonardo", "Silva", 19));
            employees.Add(new Employee("Fernanda", "Kipper", 19));
            employees.Add(new Employee("Orosman", "Silva", 72));
            employees.Add(new Employee("Regilaine", "Santos", 42));
            employees.Add(new Employee("Gabriel", "Huszcza", 22));
        }

        public static List<Employee> GetData()
        {
            return employees;
        }

        public static void AddEmployee(Employee e)
        {
            employees.Add(e);
        }

        public static void RemoveEmployee()
        {
            throw new NotImplementedException();
        }

        public static void EditEmployee()
        {
            throw new NotImplementedException();
        }

        public static void SaveEmployees()
        {
            throw new NotImplementedException();
        }

        public static string Output()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                string toAppend =
                    "*****************\n" +
                    $"Full name: {employee.FullName}\n" +
                    $"Age: {employee.Age}\n";
                sb.Append(toAppend);
            }

            return sb.ToString();
        }

    }
}
