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
    public class AircraftService
    {
        ApplicationContext bd;
        private readonly ILogger<AircraftController> logger;

        DbSet<Aircraft> aircraftBd { get => bd.Aircraft; }


        public AircraftService(ILogger<AircraftController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Aircraft>>> GetAircraftList()
        {
            return await bd.Aircraft.Include(a => a.Airline).AsNoTracking().ToListAsync();
        }

        public async Task<Aircraft> GetAircraft(int id)
        {
            Aircraft aircraft = await aircraftBd.Include(a => a.Airline).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (aircraft == null)
            {
                throw new Exception("Aircraft");
            }
            return aircraft;
        }

        public async Task<int> AddAircraft(AircraftCreate aircraft)
        {
            Aircraft newAircraft = new Aircraft
            {
                Model = aircraft.Model,
                BoardNumber = aircraft.BoardNumber,
                NumberOfSeats = aircraft.NumberOfSeats,
                YearOfRelease = aircraft.YearOfRelease,
                AirlineId = aircraft.AirlineId
            };

            if (newAircraft == null || aircraft.Model == null || aircraft.BoardNumber == null || !bd.Airline.Any(x => x.Id == aircraft.AirlineId))
            {
                throw new Exception("Aircraft");
            }

            EntityEntry<Aircraft> ent = await aircraftBd.AddAsync(newAircraft);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Aircraft");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateAircraft(AircraftUpdate aircraft)
        {
            if (aircraft == null || !aircraftBd.Any(x => x.Id == aircraft.Id) || aircraft.Model == null || aircraft.BoardNumber == null || !bd.Airline.Any(x => x.Id == aircraft.AirlineId))
            {
                throw new Exception("Aircraft");
            }

            Aircraft upAircraft = aircraftBd.Find(aircraft.Id);
            upAircraft.Model = aircraft.Model;
            upAircraft.BoardNumber = aircraft.BoardNumber;
            upAircraft.NumberOfSeats = aircraft.NumberOfSeats;
            upAircraft.YearOfRelease = aircraft.YearOfRelease;
            upAircraft.AirlineId = aircraft.AirlineId;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteAircraft(int id)
        {
            Aircraft aircraft = aircraftBd.FirstOrDefault(o => o.Id == id);
            if (aircraft == null)
            {
                throw new Exception("Aircraft");
            }

            EntityEntry<Aircraft> ent = aircraftBd.Remove(aircraft);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Aircraft");
            }
            await bd.SaveChangesAsync();
        }
    }
}
