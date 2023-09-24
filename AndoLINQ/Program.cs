using Data;
using Extensions;
using static Extensions.Extension;

namespace AndoLINQ
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            List<Employee> employeeList = await Data.Data.GetEmployees();

            // Select means 'map': 
            SelectTest(employeeList);

            WhereTest(employeeList);

            ExtensionsTest(employeeList);

            TakeTest(employeeList);

            ContainsTest(employeeList);

            List<Employee> list1 = (from emp in employeeList
                                    where emp.IsManager == true
                                    orderby emp.LastName descending, emp.FirstName  // orderby LastName then FirstName. list1.OrderBy(...).ThenBy(...).ThenBy(...).ToList().  List1.OrderByDescending(...)
                                    select emp).ToList();
            Console.WriteLine("Printing 'Select' with 'orderby' employees (IsManager, orderby emp.LastName descending, emp.FirstName");

            DisplayEmployees(list1);
        }

        private static void SelectTest(List<Employee> employeeList)
        {
            // From each employeed in employeeList, select the FirstName
            List<string> selectedEmployees1 = employeeList.Select(emp => emp.FirstName).ToList();
            Console.WriteLine("Printing 'Select' employees (emp => emp.FirstName):");
            foreach (string emp in selectedEmployees1)
            {
                Console.WriteLine($"\tEmployee first name: {emp}");
            }
            Console.WriteLine();

            // From each employeed in employeeList, select the FirstName
            List<string> selectedEmployees2 = (from emp in employeeList
                                               select emp.FirstName).ToList();
            Console.WriteLine("Printing 'Select' employees (from emp in employeeList select emp.FirstName):");
            foreach (string emp in selectedEmployees2)
            {
                Console.WriteLine($"\tEmployee first name: {emp}");
            }
            Console.WriteLine();

            // From each employeed in employeeList, select the FirstName and LastName
            var selectedEmployees3 = (from emp in employeeList
                                      select new
                                      {
                                          FirstName = emp.FirstName,
                                          LastName = emp.LastName
                                      }).ToList();
            Console.WriteLine("Printing 'Select' employees (from emp in employeeList select new {...}):");
            foreach (var emp in selectedEmployees3)
            {
                Console.WriteLine($"\tEmployee name: {emp.FirstName} {emp.LastName}");
            }
            Console.WriteLine();

            // From each employeed in employeeList, select the FirstName and LastName
            var selectedEmployees4 = employeeList.Select(emp => new
                                                        {
                                                            FirstName = emp.FirstName,
                                                            LastName = emp.LastName
                                                        }).ToList();
            Console.WriteLine("Printing 'Select' employees (emp => new {...}):");
            foreach (var emp in selectedEmployees4)
            {
                Console.WriteLine($"\tEmployee name: {emp.FirstName} {emp.LastName}");
            }
            Console.WriteLine();
        }

        private static void WhereTest(List<Employee> employeeList)
        {
            // Where means 'filter': apply the predicate (Boolean statement) and return the filtered collection
            List<Employee> wheredEmployees = employeeList.Where(emp => emp.IsManager == true).ToList();

            Console.WriteLine("Printing 'Where' employees (IsManager):");
            DisplayEmployees(wheredEmployees);

            List<Employee> wheredEmployees2 = employeeList.Where(emp => emp.FirstName.StartsWith('S') || emp.LastName.StartsWith('S')).ToList();

            Console.WriteLine("Printing 'Where' employees (FirstName or LastName starts with 'S'):");
            DisplayEmployees(wheredEmployees2);

            List<Employee> wheredEmployees3 = (from emp in employeeList
                                               where emp.FirstName.StartsWith('S') || emp.LastName.StartsWith('S')
                                               select emp).ToList();

            Console.WriteLine("Printing 'Where' employees (FirstName or LastName starts with 'S'):");
            DisplayEmployees(wheredEmployees3);
        }

        private static void TakeTest(List<Employee> employeeList)
        {
            /*
            o	Take(^5..^2) retrieves the 5th item from the end through the 3rd item from the end
            •	TakeWhile: takes an expression and retrieves those items that match the expression starting from the front of the collection
            */

            // OrderBy LastName ascending (default)
            List<Employee> takeTest = employeeList.OrderBy(emp => emp.LastName).ThenBy(emp => emp.FirstName).ToList();

            //Take(5) retrieves the first 5 items
            List<Employee> take5 = takeTest.Take(5).ToList();

            Console.WriteLine("Printing 'Take' employees (Take(5)):");
            DisplayEmployees(take5);

            // Take(5..8) retrieves the items at indexes 5, 6, 7 and stops at 8 (5 is the starting case and 8 is the stopping case)
            List<Employee> take5to8 = takeTest.Take(5..8).ToList();

            Console.WriteLine("Printing 'Take' employees (Take(5..8)):");
            DisplayEmployees(take5to8);

            // Take(..4) retrieves the items at indexes 0, 1, 2, 3 and stops at 4 (The head (0) is the starting case and 4 is the stopping case)
            List<Employee> take0to4 = takeTest.Take(..4).ToList();

            Console.WriteLine("Printing 'Take' employees (Take(..4)):");
            DisplayEmployees(take0to4);

            // Take(95..) retrieves the items at indexes 95, 96, 97, 98, 99 and stops at the end of the list (item at index 99)
            List<Employee> take95toEnd = takeTest.Take(95..).ToList();

            Console.WriteLine("Printing 'Take' employees (Take(95..)):");
            DisplayEmployees(take95toEnd);
        }

        private static void SkipTest(List<Employee> employeeList)
        {

        }

        private static void DistinctTest(List<Employee> employeeList)
        {

        }

        private static void ChunkTest(List<Employee> employeeList)
        {

        }

        private static void ContainsTest(List<Employee> employeeList)
        {
            EmployeeIdComparer employeeIdComparer = new();

            bool value = (from emp in employeeList select emp).Contains(new Employee { Id = 42}, employeeIdComparer);

            Console.WriteLine($"ContainsTest: the list {(value ? "contains" : "does not contain")} employee with ID = 42");
            Console.WriteLine();
        }

        private static void ExtensionsTest(List<Employee> employeeList)
        {
            List<Employee> departmentEmployees = employeeList.ByDepartment(2).ToList();

            Console.WriteLine("Printing 'ByDepartment' employees (ByDepartment(2)):");
            DisplayEmployees(departmentEmployees);

            // Test Extension method: Filter()
            List<Employee> filteredEmployees = employeeList.Filter(emp => emp.IsManager == true);

            Console.WriteLine("Printing 'Filter' employees (Filter(emp => emp.IsManager == true)):");
            DisplayEmployees(filteredEmployees);
        }

        private static void DisplayEmployees(IEnumerable<Employee> employees)
        {
            Console.WriteLine($"Displaying {employees.ToList().Count} employees:");

            foreach (Employee emp in employees)
            {
                Console.WriteLine($"\tFirst Name: {emp.FirstName}");
                Console.WriteLine($"\tLast Name: {emp.LastName}");
                Console.WriteLine($"\tAnnual Salary: {emp.AnnualSalary}");
                Console.WriteLine($"\tManager: {emp.IsManager}");
                Console.WriteLine($"\tDepartment: {emp.DepartmentId}");
                Console.WriteLine();
            }
        }
    }
}