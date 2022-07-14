using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerConsole.Models
{
    internal class Professor: Person
    {
        private int _workHour;
        private int _salaryPerHour;
        private string _role;
        private const int _maxWorkHour = 40;

        public Professor(string[] row)
        {
            Role = row[0];
            Sex = row[1];
            Name = row[2];
            Surname = row[3];
            WorkHours = Int32.Parse(row[6]);
            SalaryPerHour = Int32.Parse(row[7]);
        }
        public int WorkHours {
            get
            {
                return _workHour;
            }
                
            set
            {
                _workHour = value;
            }
        }
        public int SalaryPerHour {
            get
            {
                return _salaryPerHour;
            }
            set
            {
                _salaryPerHour = value;
            }
        }
        public string Role{ get; set; }
        
        public override void ShowSalary(int months)
        {
            if(WorkHours > _maxWorkHour)
                Console.WriteLine($"{Name} {Surname} {Role} {_maxWorkHour * SalaryPerHour}");
            else
                Console.WriteLine($"{Name} {Surname} {Role} {WorkHours * SalaryPerHour * months}");
        }
        public override int GetSalary(int months)
        {
            if (WorkHours > _maxWorkHour)
                return _maxWorkHour * SalaryPerHour;
            else
                return WorkHours * SalaryPerHour * months;
        }
    }
}
