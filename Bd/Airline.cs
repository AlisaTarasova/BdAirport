using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Airline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Representative { get; set; }

        //public List<Aircraft> Aircrafts { get; set; } = new List<Aircraft>();
    }

    public class AirlineCreate
    {
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Representative { get; set; }

    }
}
