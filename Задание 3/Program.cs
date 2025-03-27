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

    public override string ToString()
    {
        return $"{Name} | {Department} | {Salary}";
    }
}

public class EmployeeFileReader
{
    private char separator;

    public EmployeeFileReader(char separator)
    {
        this.separator = separator;
    }

    public List<Employee> ReadEmployees()
    {
        List<Employee> employees = new List<Employee>();
        using (StreamReader reader = new StreamReader("file.data"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(separator);
                if (parts.Length == 3 && decimal.TryParse(parts[2], out var salary))
                {
                    employees.Add(new Employee(parts[0], parts[1], salary));
                }
            }
        }
        return employees;
    }
}

public class EmployeeProcessor
{
    public List<Employee> FindByDepartment(List<Employee> employees, string department)
    {
        return employees.FindAll(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase));
    }
}

class Program
{
    static void Main()
    {
        EmployeeFileReader fileReader = new EmployeeFileReader('|');
        List<Employee> employees = fileReader.ReadEmployees();

        Console.WriteLine("Список сотрудников:");
        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }

        Console.WriteLine("\nВведите название отдела для поиска сотрудников:");
        string department = Console.ReadLine();

        EmployeeProcessor processor = new EmployeeProcessor();
        var filteredEmployees = processor.FindByDepartment(employees, department);

        Console.WriteLine($"\nСотрудники в отделе '{department}':");
        if (filteredEmployees.Count > 0)
        {
            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine(employee);
            }
        }
        else
        {
            Console.WriteLine("Сотрудники не найдены.");
        }
    }
}