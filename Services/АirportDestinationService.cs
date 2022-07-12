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
    public class АirportDestinationService
    {
        ApplicationContext bd;
        private readonly ILogger<АirportDestinationController> logger;

        DbSet<АirportDestination> airportBd { get => bd.АirportDestination; }


        public АirportDestinationService(ILogger<АirportDestinationController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<АirportDestination>>> GetAirportList()
        {
            return await airportBd.AsNoTracking().ToListAsync();
        }

        public async Task<АirportDestination> GetAirport(int id)
        {
            АirportDestination airport = await airportBd.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (airport == null)
            {
                throw new Exception("АirportDestination");
            }
            return airport;
        }

        public async Task<int> AddAirport(АirportDestinationCreate airport)
        {
            АirportDestination newAirport = new АirportDestination
            {
                City = airport.City,
                Address = airport.Address
            };

            if (newAirport == null || airport.City == null || airport.Address == null)
            {
                throw new Exception("АirportDestination");
            }

            EntityEntry<АirportDestination> ent = await airportBd.AddAsync(newAirport);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("АirportDestination");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateAirport(АirportDestination airport)
        {
            if (airport == null || !airportBd.Any(x => x.Id == airport.Id) || airport.City == null || airport.Address == null)
            {
                throw new Exception("АirportDestination");
            }

            АirportDestination upAirport = airportBd.Find(airport.Id);
            upAirport.City = airport.City;
            upAirport.Address = airport.Address;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteAirport(int id)
        {
            АirportDestination airport = airportBd.FirstOrDefault(o => o.Id == id);
            if (airport == null)
            {
                throw new Exception("АirportDestination");
            }

            EntityEntry<АirportDestination> ent = airportBd.Remove(airport);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("АirportDestination");
            }
            await bd.SaveChangesAsync();
        }
    }
}
