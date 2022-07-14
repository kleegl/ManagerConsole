using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerConsole.Models
{
    internal abstract class PersonFactory
    {
        public static List<Person> CreatePerson(List<string[]> objectList)
        {
            List<Person> persons = new List<Person>();

            foreach (string[] row in objectList)
            {
                if (row[0].Equals("Студент"))
                {
                    persons.Add(new Student(row));
                }
                if (row[0].Equals("Профессор"))
                {
                    persons.Add(new Professor(row));
                }
                if (row[0].Equals("Сотрудник"))
                {
                    persons.Add(new Employee(row));
                }
            }
            return persons;
        }
    }
}
