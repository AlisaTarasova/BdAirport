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
    public class FlightService
    {
        ApplicationContext bd;
        private readonly ILogger<FlightController> logger;

        DbSet<Flight> flightBd { get => bd.Flight; }


        public FlightService(ILogger<FlightController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Flight>>> GetFlightList()
        {
            return await bd.Flight
                .Include(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(a1 => a1.АirportDeparture)
                .Include(a2 => a2.АirportDestination)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Flight> GetFlight(int id)
        {
            Flight flight = await flightBd
                .Include(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(a1 => a1.АirportDeparture)
                .Include(a2 => a2.АirportDestination).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (flight == null)
            {
                throw new Exception("Flight");
            }
            return flight;
        }

        public async Task<int> AddFlight(FlightCreate flight)
        {
            Flight newFlight = new Flight
            {
                AirctraftId = flight.AirctraftId,
                АirportDepartureId = flight.АirportDepartureId,
                АirportDestinationId = flight.АirportDestinationId,
                FlightNumber = flight.FlightNumber,
                DepartureDateTime = flight.DepartureDateTime,
                FlightDuration = flight.FlightDuration
            };

            if (newFlight == null || !bd.Aircraft.Any(x => x.Id == flight.AirctraftId) || !bd.АirportDeparture.Any(x => x.Id == flight.АirportDepartureId) || !bd.АirportDestination.Any(x => x.Id == flight.АirportDestinationId) || flight.FlightNumber == null || flight.FlightDuration < 0)
            {
                throw new Exception("Flight");
            }

            EntityEntry<Flight> ent = await flightBd.AddAsync(newFlight);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Flight");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateFlight(FlightUpdate flight)
        {
            if (flight == null || !flightBd.Any(x => x.Id == flight.Id) || !bd.Aircraft.Any(x => x.Id == flight.AirctraftId) || !bd.АirportDeparture.Any(x => x.Id == flight.АirportDepartureId) || !bd.АirportDestination.Any(x => x.Id == flight.АirportDestinationId) || flight.FlightNumber == null || flight.FlightDuration < 0)
            {
                throw new Exception("Flight");
            }

            Flight upFlight = flightBd.Find(flight.Id);
            upFlight.AirctraftId = flight.AirctraftId;
            upFlight.АirportDepartureId = flight.АirportDepartureId;
            upFlight.АirportDestinationId = flight.АirportDestinationId;
            upFlight.FlightNumber = flight.FlightNumber;
            upFlight.DepartureDateTime = flight.DepartureDateTime;
            upFlight.FlightDuration = flight.FlightDuration;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteFlight(int id)
        {
            Flight flight = flightBd.FirstOrDefault(o => o.Id == id);
            if (flight == null)
            {
                throw new Exception("Flight");
            }

            EntityEntry<Flight> ent = flightBd.Remove(flight);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Flight");
            }
            await bd.SaveChangesAsync();
        }
    }
}
