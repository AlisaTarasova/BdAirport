using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class АirportDestination
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class АirportDestinationCreate
    {
        public string City { get; set; }
        public string Address { get; set; }
    }
}
