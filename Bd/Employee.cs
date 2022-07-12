using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Employee
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int Experience { get; set; }

        //public List<Crew> Crews { get; set; } = new List<Crew>();
    }

    public class EmployeeCreate
    {        
        public int PersonId { get; set; }
        public int Experience { get; set; }
    }

    public class EmployeeUpdate
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int Experience { get; set; }
    }
}
