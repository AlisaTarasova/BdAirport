using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Flight
    {
        public int Id { get; set; }

        public int AirctraftId { get; set; }
        public Aircraft Airctraft { get; set; }

        public int АirportDepartureId { get; set; }
        public АirportDeparture АirportDeparture { get; set; }

        public int АirportDestinationId { get; set; }
        public АirportDestination АirportDestination { get; set; }

        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int FlightDuration { get; set; }

        //public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        //public List<Crew> Crews { get; set; } = new List<Crew>();
    }

    public class FlightCreate
    {
        public int AirctraftId { get; set; }
        public int АirportDepartureId { get; set; }
        public int АirportDestinationId { get; set; }

        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int FlightDuration { get; set; }
    }

    public class FlightUpdate
    {
        public int Id { get; set; }
        public int AirctraftId { get; set; }
        public int АirportDepartureId { get; set; }
        public int АirportDestinationId { get; set; }

        public string FlightNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public int FlightDuration { get; set; }
    }
}
