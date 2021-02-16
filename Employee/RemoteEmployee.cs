using System;
using System.IO;
using System.Runtime.Serialization;

namespace EmployeeManager
{
    [Serializable]
    public class RemoteEmployee : IEmployee
    {
        public string Type { get; } = "Remote";
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
                string prefix = Type == "Remote" ? "r" : "p";
                return $"{prefix}{this.FirstName.ToLower()}_{this.LastName.ToLower()}{this.Age}";
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

        }

        public RemoteEmployee() { }

        public RemoteEmployee(string firstName, string lastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FN", this.FirstName);
            info.AddValue("LN", this.LastName);
            info.AddValue("AGE", this.Age);
            info.AddValue("TYPE", this.Type);
        }

        public RemoteEmployee(SerializationInfo info, StreamingContext context)
        {
            this.FirstName = info.GetString("FN");
            this.LastName = info.GetString("LN");
            this.Age = info.GetInt32("AGE");
            this.Type = info.GetString("TYPE");
        }

        public override string ToString()
        {
            return $"\n*******************\nFull name: {FullName}\nAge: {Age}\nID: {ID}\nType: {Type}\n*******************\n";
        }
    }
}
