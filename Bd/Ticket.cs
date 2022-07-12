using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Ticket
    {
        public int Id { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public string Class { get; set; }
        public string Place { get; set; }        
        public bool BaggageAvailability { get; set; }
        public int Price { get; set; }

        //public virtual BookingTicket BookingTicket { get; set; }
    }

    public class TicketCreate
    {
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public string Class { get; set; }
        public string Place { get; set; }
        public bool BaggageAvailability { get; set; }
        public int Price { get; set; }
    }

    public class TicketUpdate
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public string Class { get; set; }
        public string Place { get; set; }
        public bool BaggageAvailability { get; set; }
        public int Price { get; set; }
    }
}
