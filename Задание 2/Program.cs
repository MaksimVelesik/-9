using System;
using System.Collections.Generic;
using System.IO;

public class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, string department, decimal salary)
    {
        Name = name;
        Department = department;
        Salary = salary;
    }
}

public class EmployeeFileWriter
{
    public void WriteEmployees(List<Employee> employees, char separator)
    {
        using (StreamWriter writer = new StreamWriter("file.data"))
        {
            foreach (var employee in employees)
            {
                string line = $"{employee.Name}{separator}{employee.Department}{separator}{employee.Salary}";
                writer.WriteLine(line);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee("Alice Smith", "HR", 50000),
            new Employee("Bob Johnson", "IT", 60000),
            new Employee("Charlie Brown", "Finance", 55000)
        };

        EmployeeFileWriter fileWriter = new EmployeeFileWriter();
        fileWriter.WriteEmployees(employees, '|');

        Console.WriteLine("Данные сотрудников записаны в файл file.data.");
    }
}