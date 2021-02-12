﻿using EmployeeManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DatabaseManager
{
    public static class Database
    {

        private static List<Employee> employees = new List<Employee>();

        static Database()
        {
            UpdateDatabase();
        }

        private static void UpdateDatabase()
        {
            var cwd = Directory.GetCurrentDirectory();

            string[] dirs = Directory.GetDirectories(cwd);

            foreach (var dir in dirs)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);

                string fullPath = Path.Combine(dir, dirInfo.Name);

                Employee e = OpenDATFile(fullPath);

                employees.Add(e);
            }
        }

        private static void CreateSampleData()
        {
            employees.Add(new Employee("Leonardo", "Silva", 19));
            employees.Add(new Employee("Fernanda", "Kipper", 19));
            employees.Add(new Employee("Orosman", "Silva", 72));
            employees.Add(new Employee("Regilaine", "Santos", 42));
            employees.Add(new Employee("Gabriel", "Huszcza", 22));

            SaveDatabase();
        }

        public static List<Employee> GetData()
        {
            return employees;
        }

        public static string AddEmployee(Employee e)
        {
            employees.Add(e);
            return "Employee added successfully";
        }

        public static string RemoveEmployee(Employee e)
        {

            Employee employee = employees.Where(x => x.Age == e.Age && x.FullName.ToLower().Contains(e.FullName.ToLower())).ToList()[0];
            string message = "Deleted successfully";

            if (employees.Remove(employee))
            {
                try
                {
                    File.Delete(Path.Combine(employee.LocalPath, $"{employee.ID}.dat"));
                    Directory.Delete(employee.LocalPath);
                }
                catch (Exception ex)
                {
                    message = $"Unsuccessfully deleted {ex.Message}";
                }
            }

            return message;

        }

        public static string RemoveEmployee(string id)
        {
            // handle excpetion

            Employee employee = employees.Where(x => x.ID == id).ToList()[0];
            string message = "Deleted successfully";

            if (employees.Remove(employee))
            {
                try
                {
                    File.Delete(Path.Combine(employee.LocalPath, $"{employee.ID}.dat"));
                    Directory.Delete(employee.LocalPath);
                }
                catch (Exception ex)
                {
                    message = $"Unsuccessfully deleted {ex.Message}";
                }
            }

            return message;

        }

        public static void EditEmployee()
        {
            throw new NotImplementedException();
        }

        public static string SaveDatabase()
        {
            try
            {
                employees.ForEach(e => CreateDATFile(e));
            }
            catch (Exception)
            {
                return "Unsuccessfully saved the database";
                throw;
            }
            return "Successfully saved the database";
        }

        private static bool CreateDATFile(Employee employee)
        {

            if (!(Directory.Exists(employee.LocalPath)))
            {
                CreateEmployeeFolder(employee);
            }

            Stream stream = File.Open($"{Path.Combine(employee.LocalPath, employee.ID)}.dat", FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, employee);

            stream.Close();

            return true;
        }

        private static Employee OpenDATFile(Employee employee)
        {

            Stream stream = File.Open($"{Path.Combine(employee.LocalPath, employee.ID)}.dat", FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            var e = (Employee)bf.Deserialize(stream);

            stream.Close();

            return e;
        }

        private static Employee OpenDATFile(string path)
        {

            Stream stream = File.Open($"{path}.dat", FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            var e = (Employee)bf.Deserialize(stream);

            stream.Close();

            return e;
        }

        public static string Output()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                string toAppend = $"*****************\n{employee.ToString()}";
                sb.Append(toAppend);
            }

            return sb.ToString();
        }

        public static bool ExistsEmployeeFolder(Employee employee)
        {
            var cwd = Directory.GetCurrentDirectory();
            var exists = Directory.Exists(Path.Combine(cwd, employee.ID));

            return exists;
        }

        public static bool CreateEmployeeFolder(Employee employee)
        {
            var cwd = Directory.GetCurrentDirectory();
            var dirInfo = Directory.CreateDirectory(Path.Combine(cwd, employee.ID));

            return dirInfo.Exists;
        }
    }
}
