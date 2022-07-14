using ExcelDataReader;
using System.Data;
using System.IO;
using System;
using ManagerConsole.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace MangerConsole
{
    internal class Program
    {
        public static DataTable DATATABLE = new DataTable();
        public static List<DataRow> LISTROWS = new List<DataRow>();
        public static List<string[]> OBJECTLIST = new List<string[]>();

        public static List<Person> PERSONS = new List<Person>();
        
        public static List<Student> STUDENTS = new List<Student>();
        public static List<Professor> PROFESSORS = new List<Professor>();
        public static List<Employee> EMPLOYEES = new List<Employee>();

        //public static object Parse { get; private set; }

        static void Main(string[] args)
        {
            ImportExcel();
            GetPersonsList();

            bool state = true;
            while (state)
            {
                Console.WriteLine("\nMenu: ");
                Console.WriteLine("1. Show Data Salary \n" +
                                  "2. Show Max Salary Behind Six Month\n" +
                                  "3. Excellent Students \n" +
                                  "4. Max fixed salary \n" +
                                  "5. Average Rating \n" +
                                  "6. Exit\n");
                string key = Console.ReadLine();
                switch (key)
                {
                    case ("1"):
                        GetPersonsSalary();
                        break;
                    case ("2"):
                        MaxSalaryBehindSixMouth();
                        break;
                    case ("3"):
                        ExcellentStudet();
                        break;
                    case ("4"):
                        MaxFixedSalary();
                        break;
                    case ("5"):
                        AverageRating();
                        break;
                    case ("6"):
                        state = false;
                        break;
                }
            }
        }

        public static void ImportExcel()
        {
            Console.WriteLine("Write path to Student3.xlsx file");
            string path = Console.ReadLine().Replace("\"\"", "\"");
            FileStream fStream = File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fStream);
            DataSet resultDataSet = excelDataReader.AsDataSet();
            excelDataReader.Close();
            DATATABLE = resultDataSet.Tables[0];
        }
        //create PERSONS list
        public static void GetPersonsList()
        {
            Console.Clear();
            foreach (DataRow rows in DATATABLE.Rows)
            {
                LISTROWS.Add(rows);
            }
            foreach(var row in LISTROWS)
            {
                string[] arr = new string[9];
                int counter = 0;
                foreach(var r in row.ItemArray)
                {
                    arr.SetValue(r.ToString(), counter);
                    counter++;
                }
                OBJECTLIST.Add(arr);
            }
            PERSONS = PersonFactory.CreatePerson(OBJECTLIST);
            
            foreach(var person in PERSONS)
            {
                if (person.GetType() == typeof(Student))
                    STUDENTS.Add((Student)person);
                else if (person.GetType() == typeof(Professor))
                    PROFESSORS.Add((Professor)person);
                else if (person.GetType() == typeof(Employee))
                    EMPLOYEES.Add((Employee)person);
            }
        }
        //write data
        public static void GetPersonsSalary()
        {
            Console.WriteLine("Input number of months: \n");
            int months = Int32.Parse(Console.ReadLine());

            foreach(var person in PERSONS)
                person.ShowSalary(months);
            SaveData(months);
        }
        public static void SaveData(int months)
        {
            Console.WriteLine("\nDo you want to save data?   Y/N");
            string answer = Console.ReadLine();
            if (answer.Equals("Y") || answer.Equals("y"))
            {
                //string path = @"C:\Users\Jenya Mefedov\Desktop\data\persons.txt";
                Console.WriteLine("\nEnter path for save");
                string path = Console.ReadLine().Replace("\"\"", "\"");
                try
                {
                    StreamWriter writer = new StreamWriter(path);
                    foreach (var person in PERSONS)
                    {
                        if (person.GetType() == typeof(Student))
                        {
                            Student student = (Student)person;
                            writer.Write($"{student.Name};{student.Surname};{student.Role};{student.GetSalary(months)};\n");
                        }
                        else if (person.GetType() == typeof(Professor))
                        {
                            Professor profesor = (Professor)person;
                            writer.Write($"{profesor.Name};{profesor.Surname};{profesor.Role};{profesor.GetSalary(months)};\n");
                        }
                        else if (person.GetType() == typeof(Employee))
                        {
                            Employee employee = (Employee)person;
                            writer.Write($"{employee.Name};{employee.Surname};{employee.Role};{employee.GetSalary(months)};\n");
                        }
                    }
                    writer.Close();
                    Console.WriteLine($"Dates was saved in {path}!");
                }
                catch (DirectoryNotFoundException)
                {
                     Console.WriteLine("This path was not found!");
                }
            }
        }
        public static void MaxSalaryBehindSixMouth()
        {
            const int sixMounth = 6;
            int maxSalary = 0;
            foreach(var person in PERSONS)
            {
                if(person.GetSalary(sixMounth) > maxSalary)
                    maxSalary = person.GetSalary(sixMounth);
            }
            Console.WriteLine(maxSalary);
        }
        public static void ExcellentStudet()
        {
            int counter = 0;
            foreach(var student in STUDENTS)
            {
                if (student.ReportCard.Contains("3") || student.ReportCard.Contains("4"))
                    continue;
                counter++;
            }
            Console.WriteLine("Excellent studet count " + counter);
        }
        public static void MaxFixedSalary()
        {
            Employee employeeWithMaxFixedSalary = new Employee() { Name="", Surname="", FixedSalary=0, Sex="", Role=""};
            foreach (var employee in EMPLOYEES)
            {
                if (employeeWithMaxFixedSalary.FixedSalary < employee.FixedSalary)
                    employeeWithMaxFixedSalary = employee;
            }
            Console.WriteLine($"Max fixed salary has {employeeWithMaxFixedSalary.Name} {employeeWithMaxFixedSalary.Surname} \n" +
                $"Max fixed salary = {employeeWithMaxFixedSalary.FixedSalary}");
        }
        public static void AverageRating()
        {
            foreach (var student in STUDENTS)
            {
                string[] s = student.ReportCard.Split(',');
                int[] rating = new int[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    rating[i] = Int32.Parse(s[i]);
                }
                Console.WriteLine($"{student.Name} {student.Surname} has average rating {rating.Sum() / rating.Length}");
            }
        }
    }
}