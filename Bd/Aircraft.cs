using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Aircraft
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string BoardNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public int YearOfRelease { get; set; }
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }

        //public List<Flight> Flights { get; set; } = new List<Flight>();
    }

    public class AircraftCreate
    {
        public string Model { get; set; }
        public string BoardNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public int YearOfRelease { get; set; }
        public int AirlineId { get; set; }
    }

    public class AircraftUpdate
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string BoardNumber { get; set; }
        public int NumberOfSeats { get; set; }
        public int YearOfRelease { get; set; }
        public int AirlineId { get; set; }
    }
}
