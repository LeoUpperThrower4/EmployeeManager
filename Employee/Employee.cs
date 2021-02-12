using System;
using System.IO;
using System.Runtime.Serialization;

namespace EmployeeManager
{
    [Serializable]
    public class Employee : ISerializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }
        public string ID
        {
            get
            {
                return $"{this.FirstName.ToLower()}_{this.LastName.ToLower()}{this.Age}";
            }
        }
        public string LocalPath
        {
            get
            {
                string cwd = Directory.GetCurrentDirectory();
                string fullPath = Path.Combine(cwd, this.ID);

                return fullPath;
            }
            set
            { }//funciona sem nada?

        }

        public Employee() { }

        public Employee(string firstName, string lastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FN", this.FirstName);// add obj info in here
            info.AddValue("LN", this.LastName);// add obj info in here
            info.AddValue("AGE", this.Age);// add obj info in here
        }

        public Employee(SerializationInfo info, StreamingContext context)
        {
            this.FirstName = info.GetString("FN");
            this.LastName = info.GetString("LN");
            this.Age = info.GetInt32("AGE");
        }

        public override string ToString()
        {
            return $"Full name: {FullName}\nAge: {Age}\nID: {ID}\n";
        }
    }
}
