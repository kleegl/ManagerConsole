using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerConsole.Models
{
    internal class Employee : Person
    {
        private int _fixedSalary;
        private string _role;
        private const int _workHour = 8;
        public Employee(string[] row)
        {
            Role = row[0];
            Sex = row[1];
            Name = row[2];
            Surname = row[3];
            FixedSalary = Int32.Parse(row[8]);
        }

        public Employee()
        {
        }

        public int FixedSalary {
            get
            {
                return _fixedSalary;
            }
            set
            { 
                _fixedSalary = value;
            }
        }
        public string Role { get; set; }
        public override void ShowSalary(int months)
        {
            Console.WriteLine($"{Name} {Surname} {Role} {FixedSalary * _workHour * months}");
        }
        public override int GetSalary(int months) => FixedSalary * _workHour * months;
    }
}
