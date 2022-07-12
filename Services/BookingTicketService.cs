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
    public class BookingTicketService
    {
        ApplicationContext bd;
        private readonly ILogger<BookingTicketController> logger;

        DbSet<BookingTicket> bookingTicketBd { get => bd.BookingTicket; }


        public BookingTicketService(ILogger<BookingTicketController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<BookingTicket>>> GetBookingTicketList()
        {
            return await bd.BookingTicket
                .Include(t => t.Ticket).ThenInclude(f => f.Flight).ThenInclude(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(t => t.Ticket).ThenInclude(f => f.Flight).ThenInclude(a1 => a1.АirportDeparture)
                .Include(t => t.Ticket).ThenInclude(f => f.Flight).ThenInclude(a2 => a2.АirportDestination)
                .Include(t => t.Ticket).ThenInclude(p => p.Passenger).ThenInclude(p => p.Person)
                .AsNoTracking().ToListAsync();
        }

        public async Task<BookingTicket> GetBookingTicket(int id)
        {
            BookingTicket bookingTicket = await bookingTicketBd
                .Include(t => t.Ticket).ThenInclude(f => f.Flight).ThenInclude(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(t => t.Ticket).ThenInclude(f => f.Flight).ThenInclude(a1 => a1.АirportDeparture)
                .Include(t => t.Ticket).ThenInclude(f => f.Flight).ThenInclude(a2 => a2.АirportDestination)
                .Include(t => t.Ticket).ThenInclude(p => p.Passenger).ThenInclude(p => p.Person)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (bookingTicket == null)
            {
                throw new Exception("BookingTicket");
            }
            return bookingTicket;
        }

        public async Task<int> AddBookingTicket(BookingTicketCreate bookingTicket)
        {
            BookingTicket newBookingTicket = new BookingTicket
            {
                TicketId = bookingTicket.TicketId,
                Prepayment = bookingTicket.Prepayment,
                BookingDate = bookingTicket.BookingDate,
                BookingPeriod = bookingTicket.BookingPeriod
            };

            if (newBookingTicket == null || bookingTicket.Prepayment < 0 || !bd.Ticket.Any(x => x.Id == bookingTicket.TicketId))
            {
                throw new Exception("BookingTicket");
            }

            EntityEntry<BookingTicket> ent = await bookingTicketBd.AddAsync(newBookingTicket);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("BookingTicket");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateBookingTicket(BookingTicketUpdate bookingTicket)
        {
            if (bookingTicket == null || !bookingTicketBd.Any(x => x.Id == bookingTicket.Id) || bookingTicket.Prepayment < 0 || !bd.Ticket.Any(x => x.Id == bookingTicket.TicketId))
            {
                throw new Exception("BookingTicket");
            }

            BookingTicket upBookingTicket = bookingTicketBd.Find(bookingTicket.Id);
            upBookingTicket.TicketId = bookingTicket.TicketId;
            upBookingTicket.Prepayment = bookingTicket.Prepayment;
            upBookingTicket.BookingDate = bookingTicket.BookingDate;
            upBookingTicket.BookingPeriod = bookingTicket.BookingPeriod;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteBookingTicket(int id)
        {
            BookingTicket bookingTicket = bookingTicketBd.FirstOrDefault(o => o.Id == id);
            if (bookingTicket == null)
            {
                throw new Exception("BookingTicket");
            }

            EntityEntry<BookingTicket> ent = bookingTicketBd.Remove(bookingTicket);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("BookingTicket");
            }
            await bd.SaveChangesAsync();
        }
    }
}
