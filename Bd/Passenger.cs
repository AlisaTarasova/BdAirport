using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Bd
{
    public class Passenger
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string PhoneNumber { get; set; }

        //string result = string.Format("{0:+# (###) ###-##-##}", number);

        //public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
    public class PassengerCreate
    {        
        public int PersonId { get; set; }        
        public string PhoneNumber { get; set; }
    }

    public class PassengerUpdate
    {
        public int Id { get; set; }
        public int PersonId { get; set; }        
        public string PhoneNumber { get; set; }
    }
    }
