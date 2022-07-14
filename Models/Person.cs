using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerConsole.Models
{
    internal class Person: IPerson
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Surname { get; set; }

        public virtual void ShowSalary(int months) {}
        public virtual int GetSalary(int months) { return 0; }
    }
}
