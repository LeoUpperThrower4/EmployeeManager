using EmployeeManager;
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

        private static Employees _employees = new Employees();

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

                IEmployee e = OpenDATFile(fullPath);

                _employees.Add(e);
            }
        }

        private static void CreateSampleData()
        {
            _employees.Add(new PhysicalEmployee("Leonardo", "Silva", 19));
            _employees.Add(new PhysicalEmployee("Fernanda", "Kipper", 19));
            _employees.Add(new PhysicalEmployee("Orosman", "Silva", 72));
            _employees.Add(new PhysicalEmployee("Regilaine", "Santos", 42));
            _employees.Add(new RemoteEmployee("Gabriel", "Huszcza", 22));

            SaveDatabase();
        }

        public static Employees GetData()
        {
            UpdateDatabase();
            return _employees;
        }

        public static string AddEmployee(IEmployee e)
        {
            if (!ExistsEmployeeFolder(e))
            {
                _employees.Add(e);
                SaveDatabase();

                return $"\n*********\nEmployee added successfully.\n*********\n"; ;
            }

            return $"\n*********\nEmployee already exists.\n*********\n"; ;

        }

        public static List<IEmployee> SearchEmployee(string id)
        {
            return _employees.Where(employee => employee.ID == id).ToList();
        }

        public static List<IEmployee> SearchEmployee(IEmployee e)
        {
            return _employees.Where(employee => employee.Age == e.Age && employee.FirstName.ToLower().Contains(e.FirstName.ToLower()) && employee.LastName.ToLower().Contains(e.LastName.ToLower())).ToList();
        }

        public static string RemoveEmployee(string id)
        {

            IEmployee employee = _employees.Where(x => x.ID == id).ToList()[0];
            string message = $"\n *********\nDeleted successfully.\n *********\n";

            if (_employees.Remove(employee))
            {
                try
                {
                    File.Delete(Path.Combine(employee.LocalPath, $"{employee.ID}.dat"));
                    Directory.Delete(employee.LocalPath);
                }
                catch (Exception ex)
                {
                    message = $"Unsuccessfully deleted {ex}";
                }
            }

            return message;

        }

        public static string RemoveEmployee(IEmployee e)
        {

            IEmployee employee = _employees.Where(employee => employee.Age == e.Age && employee.FirstName.ToLower().Contains(e.FirstName.ToLower()) && employee.LastName.ToLower().Contains(e.LastName.ToLower())).ToList()[0];
            string message = $"\n *********\nDeleted successfully.\n *********\n";

            if (_employees.Remove(employee))
            {
                try
                {
                    File.Delete(Path.Combine(employee.LocalPath, $"{employee.ID}.dat"));
                    Directory.Delete(employee.LocalPath);
                }
                catch (Exception ex)
                {
                    message = $"Unsuccessfully deleted {ex}";
                }
            }

            return message;

        }

        public static string SaveDatabase()
        {
            try
            {
                foreach (IEmployee employee in _employees)
                {
                    CreateDATFile(employee);
                }
            }
            catch (Exception e)
            {
                return $"Unsuccessfully saved the database. {e.Message}";
            }
            return "Successfully saved the database";
        }

        private static bool CreateDATFile(IEmployee employee)
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

        private static IEmployee OpenDATFile(IEmployee employee)
        {

            Stream stream = File.Open($"{Path.Combine(employee.LocalPath, employee.ID)}.dat", FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            var e = (IEmployee)bf.Deserialize(stream);

            stream.Close();

            return e;
        }

        private static IEmployee OpenDATFile(string path)
        {

            Stream stream = File.Open($"{path}.dat", FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            var e = (IEmployee)bf.Deserialize(stream);

            stream.Close();

            return e;
        }

        public static string FullOutput()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var employee in _employees)
            {
                string toAppend = employee.ToString();
                sb.Append(toAppend);
            }

            return sb.ToString();
        }

        public static bool ExistsEmployeeFolder(IEmployee employee)
        {
            var cwd = Directory.GetCurrentDirectory();
            var exists = Directory.Exists(Path.Combine(cwd, employee.ID));

            return exists;
        }

        public static bool CreateEmployeeFolder(IEmployee employee)
        {
            var cwd = Directory.GetCurrentDirectory();
            var dirInfo = Directory.CreateDirectory(Path.Combine(cwd, employee.ID));

            return dirInfo.Exists;
        }
    }
}
