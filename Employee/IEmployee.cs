using System.Runtime.Serialization;

namespace EmployeeManager
{
    public interface IEmployee : ISerializable
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string FullName { get; }
        public string LocalPath { get; }
        public string ID { get; }
        public string Type { get; }

        public string ToString();

    }
}
