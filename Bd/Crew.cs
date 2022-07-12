using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Crew
    {
        public int Id { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }

    public class CrewCreate
    {
        public int FlightId { get; set; }
        public int EmployeeId { get; set; }
        public int PostId { get; set; }
    }

    public class CrewUpdate
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int EmployeeId { get; set; }
        public int PostId { get; set; }
    }
}