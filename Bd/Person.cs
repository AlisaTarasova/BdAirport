using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Person
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }       
        public int Age { get; set; }
        public string Passport { get; set; }

        //public Passenger Passenger { get; set; }
        //public Employee Employee { get; set; }
    }

    public class PersonCreate
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }    
        public string Passport { get; set; }
    }
}
