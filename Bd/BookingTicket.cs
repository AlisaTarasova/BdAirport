using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class BookingTicket
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int Prepayment { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingPeriod { get; set; }        
    }

    public class BookingTicketCreate
    {
        public int TicketId { get; set; }
        public int Prepayment { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingPeriod { get; set; }
    }

    public class BookingTicketUpdate
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int Prepayment { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingPeriod { get; set; }
    }
}
