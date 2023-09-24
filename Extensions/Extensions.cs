using Data;
using System.Diagnostics.CodeAnalysis;

namespace Extensions
{
    public static class Extension
    {
        public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
        {
            List<T> filteredList = new List<T>();

            foreach (T record in records)
            {
                if (func(record))
                {
                    filteredList.Add(record);
                }
            }

            return filteredList;
        }

        public static IEnumerable<Employee> ByDepartment(this IEnumerable<Employee> records, int department)
        {
            return records.Where(emp => emp.DepartmentId == department);
        }

        public class EmployeeIdComparer : EqualityComparer<Employee>
        {
            public override bool Equals(Employee x, Employee y)
            {
                return (x.Id == y.Id);
            }

            // This is used to identify equality of an object.
            // Since every employee ID is unique, we can simply return the Id hash code.
            // If IDs were not unique, we'd need to return the hash of every property
            // by creating concatinated string containing the values of all properties and
            // calling GetHashCode() on that string
            public override int GetHashCode([DisallowNull] Employee obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}