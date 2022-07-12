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
    public class AirlineService
    {
        ApplicationContext bd;
        private readonly ILogger<AirlineController> logger;

        DbSet<Airline> airlineBd { get => bd.Airline; }


        public AirlineService(ILogger<AirlineController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Airline>>> GetAirlineList()
        {
            return await airlineBd.AsNoTracking().ToListAsync();
        }

        public async Task<Airline> GetAirline(int id)
        {
            Airline airline = await airlineBd.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (airline == null)
            {
                throw new Exception("Airline");
            }
            return airline;
        }

        public async Task<int> AddAirline(AirlineCreate airline)
        {
            Airline newAirline = new Airline
            {
                Name = airline.Name,
                DateOfCreation = airline.DateOfCreation,
                Representative = airline.Representative
            };

            if (newAirline == null || airline.Name == null || airline.Representative == null)
            {
                throw new Exception("Airline");
            }

            EntityEntry<Airline> ent = await airlineBd.AddAsync(newAirline);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Airline");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateAirline(Airline airline)
        {
            if (airline == null || !airlineBd.Any(x => x.Id == airline.Id) || airline.Name == null || airline.Representative == null)
            {
                throw new Exception("Airline");
            }

            Airline upAirline = airlineBd.Find(airline.Id);
            upAirline.Name = airline.Name;
            upAirline.DateOfCreation = airline.DateOfCreation;
            upAirline.Representative = airline.Representative;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteAirline(int id)
        {
            Airline airline = airlineBd.FirstOrDefault(o => o.Id == id);
            if (airline == null)
            {
                throw new Exception("Airline");
            }

            EntityEntry<Airline> ent = airlineBd.Remove(airline);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Airline");
            }
            await bd.SaveChangesAsync();
        }
    }
}
