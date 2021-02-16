using System.Collections;
using System.Collections.Generic;

namespace EmployeeManager
{
    public class Employees : IEnumerable<IEmployee>
    {
        private List<IEmployee> _employees = new List<IEmployee>();

        public void Add(IEmployee e)
        {
            _employees.Add(e);
        }

        public bool Remove(IEmployee employee)
        {
            return _employees.Remove(employee) ? true : false;
        }

        public IEnumerator<IEmployee> GetEnumerator()
        {
            return ((IEnumerable<IEmployee>)_employees).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_employees).GetEnumerator();
        }

    }
}
