using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerConsole.Models
{
    internal class Student: Person
    {
        private string _role;
        private int _scholarship;
        private string _reportCard;

        public Student(string[] row) 
        {
            Role = row[0];
            Sex = row[1];
            Name = row[2];
            Surname = row[3];
            Scholarship = Int32.Parse(row[4]);
            ReportCard = row[5];
        }
        public string Role { 
            get
            {
                return _role;
            } 
            set
            {
                _role = value;
            } 
        }
        public int Scholarship {
            get
            {
                return _scholarship;
            }
            set
            {
                _scholarship = value;
            } 
        }
        public string ReportCard {
            get
            {
                return _reportCard;
            }
            set
            {
                _reportCard = value;
            }
        }

        public override void ShowSalary(int months)
        {
            for (int i = 0; i < ReportCard.Length; i++)
            {
                if (ReportCard[i].Equals("3"))
                {
                    Console.WriteLine($"{Name} {Surname} {Role} 0");
                    return;
                }
            }
            Console.WriteLine($"{Name} {Surname} {Role} {Scholarship * months}");
        }
        public override int GetSalary(int months)
        {
            for (int i = 0; i < ReportCard.Length; i++)
            {
                if (ReportCard[i].Equals("3"))
                    return 0;
            }
            return Scholarship * months;
        }
    }
}
