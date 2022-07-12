using BdAirport.Bd;
using BdAirport.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Services
{
    public class TicketService
    {
        ApplicationContext bd;
        private readonly ILogger<TicketController> logger;

        DbSet<Ticket> ticketBd { get => bd.Ticket; }


        public TicketService(ILogger<TicketController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketList()
        {
            return await bd.Ticket
                .Include(f => f.Flight).ThenInclude(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(f => f.Flight).ThenInclude(a1 => a1.АirportDeparture)
                .Include(f => f.Flight).ThenInclude(a2 => a2.АirportDestination)
                .Include(p => p.Passenger).ThenInclude(p => p.Person)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Ticket> GetTicket(int id)
        {
            Ticket ticket = await ticketBd
                .Include(f => f.Flight).ThenInclude(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(f => f.Flight).ThenInclude(a1 => a1.АirportDeparture)
                .Include(f => f.Flight).ThenInclude(a2 => a2.АirportDestination)
                .Include(p => p.Passenger).ThenInclude(p => p.Person)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
            {
                throw new Exception("Ticket");
            }
            return ticket;
        }

        public async Task<int> AddTicket(TicketCreate ticket)
        {
            Ticket newTicket = new Ticket
            {
                FlightId = ticket.FlightId,
                PassengerId = ticket.PassengerId,
                Class = ticket.Class,
                Place = ticket.Place,
                BaggageAvailability = ticket.BaggageAvailability,
                Price = ticket.Price
            };

            if (newTicket == null || ticket.Price < 0 || ticket.Class == null || ticket.Place == null || !bd.Flight.Any(x => x.Id == ticket.FlightId) || !bd.Passenger.Any(x => x.Id == ticket.PassengerId))
            {
                throw new Exception("Ticket");
            }

            EntityEntry<Ticket> ent = await ticketBd.AddAsync(newTicket);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Ticket");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateTicket(TicketUpdate ticket)
        {
            if (ticket == null || !ticketBd.Any(x => x.Id == ticket.Id) || ticket.Price < 0 || ticket.Class == null || ticket.Place == null || !bd.Flight.Any(x => x.Id == ticket.FlightId) || !bd.Passenger.Any(x => x.Id == ticket.PassengerId))
            {
                throw new Exception("Ticket");
            }

            Ticket upTicket = ticketBd.Find(ticket.Id);
            upTicket.FlightId = ticket.FlightId;
            upTicket.PassengerId = ticket.PassengerId;
            upTicket.Class = ticket.Class;
            upTicket.Place = ticket.Place;
            upTicket.BaggageAvailability = ticket.BaggageAvailability;
            upTicket.Price = ticket.Price;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteTicket(int id)
        {
            Ticket ticket = ticketBd.FirstOrDefault(o => o.Id == id);
            if (ticket == null)
            {
                throw new Exception("Ticket");
            }

            EntityEntry<Ticket> ent = ticketBd.Remove(ticket);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Ticket");
            }
            await bd.SaveChangesAsync();
        }
    }
}
