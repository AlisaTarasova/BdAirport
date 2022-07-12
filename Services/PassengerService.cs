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
    public class PassengerService
    {
        ApplicationContext bd;
        private readonly ILogger<PassengerController> logger;

        DbSet<Passenger> passengerBd { get => bd.Passenger; }


        public PassengerService(ILogger<PassengerController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassengerList()
        {
            return await bd.Passenger.Include(p => p.Person).AsNoTracking().ToListAsync();
        }

        public async Task<Passenger> GetPassenger(int id)
        {
            Passenger passenger = await passengerBd.Include(p => p.Person).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (passenger == null)
            {
                throw new Exception("Passenger");
            }
            return passenger;
        }

        public async Task<int> AddPassenger(PassengerCreate passenger)
        {
            Passenger newPassenger = new Passenger
            {
                PersonId = passenger.PersonId,
                PhoneNumber = passenger.PhoneNumber,
            };

            if (newPassenger == null || passenger.PhoneNumber == null || !bd.Person.Any(x => x.Id == passenger.PersonId))
            {
                throw new Exception("Passenger");
            }

            EntityEntry<Passenger> ent = await passengerBd.AddAsync(newPassenger);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Passenger");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdatePassenger(PassengerUpdate passenger)
        {
            if (passenger == null || !passengerBd.Any(x => x.Id == passenger.Id) || passenger.PhoneNumber == null || !bd.Person.Any(x => x.Id == passenger.PersonId))
            {
                throw new Exception("Passenger");
            }

            Passenger upPassenger = passengerBd.Find(passenger.Id);
            upPassenger.PersonId = passenger.PersonId;
            upPassenger.PhoneNumber = passenger.PhoneNumber;

            await bd.SaveChangesAsync();
        }

        public async Task DeletePassenger(int id)
        {
            Passenger passenger = passengerBd.FirstOrDefault(o => o.Id == id);
            if (passenger == null)
            {
                throw new Exception("Passenger");
            }

            EntityEntry<Passenger> ent = passengerBd.Remove(passenger);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Passenger");
            }
            await bd.SaveChangesAsync();
        }    
    }
}
