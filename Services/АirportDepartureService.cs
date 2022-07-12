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
    public class АirportDepartureService
    {
        ApplicationContext bd;
        private readonly ILogger<АirportDepartureController> logger;

        DbSet<АirportDeparture> airportBd { get => bd.АirportDeparture; }


        public АirportDepartureService(ILogger<АirportDepartureController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<АirportDeparture>>> GetAirportList()
        {
            return await airportBd.AsNoTracking().ToListAsync();
        }

        public async Task<АirportDeparture> GetAirport(int id)
        {
            АirportDeparture airport = await airportBd.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (airport == null)
            {
                throw new Exception("АirportDeparture");
            }
            return airport;
        }

        public async Task<int> AddAirport(АirportDepartureCreate airport)
        {
            АirportDeparture newAirport = new АirportDeparture
            {
                City = airport.City,
                Address = airport.Address
            };

            if (newAirport == null || airport.City == null || airport.Address == null)
            {
                throw new Exception("АirportDeparture");
            }

            EntityEntry<АirportDeparture> ent = await airportBd.AddAsync(newAirport);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("АirportDeparture");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateAirport(АirportDeparture airport)
        {
            if (airport == null || !airportBd.Any(x => x.Id == airport.Id) || airport.City == null || airport.Address == null)
            {
                throw new Exception("АirportDeparture");
            }

            АirportDeparture upAirport = airportBd.Find(airport.Id);
            upAirport.City = airport.City;
            upAirport.Address = airport.Address;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteAirport(int id)
        {
            АirportDeparture airport = airportBd.FirstOrDefault(o => o.Id == id);
            if (airport == null)
            {
                throw new Exception("АirportDeparture");
            }
            
            EntityEntry<АirportDeparture> ent = airportBd.Remove(airport);
            EntityState st = ent.State;
            
            if (st != EntityState.Deleted)
            {
                throw new Exception("АirportDeparture");
            }
            await bd.SaveChangesAsync();
        }
    }
}
