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
    public class CrewService
    {
        ApplicationContext bd;
        private readonly ILogger<CrewController> logger;

        DbSet<Crew> crewBd { get => bd.Crew; }


        public CrewService(ILogger<CrewController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Crew>>> GetCrewList()
        {
            return await bd.Crew
                .Include(f => f.Flight).ThenInclude(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(f => f.Flight).ThenInclude(a1 => a1.АirportDeparture)
                .Include(f => f.Flight).ThenInclude(a2 => a2.АirportDestination)
                .Include(emp => emp.Employee).ThenInclude(per => per.Person)
                .Include(p => p.Post)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Crew> GetCrew(int id)
        {
            Crew crew = await crewBd
                .Include(f => f.Flight).ThenInclude(a => a.Airctraft).ThenInclude(air => air.Airline)
                .Include(f => f.Flight).ThenInclude(a1 => a1.АirportDeparture)
                .Include(f => f.Flight).ThenInclude(a2 => a2.АirportDestination)
                .Include(emp => emp.Employee).ThenInclude(per => per.Person)
                .Include(p => p.Post).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (crew == null)
            {
                throw new Exception("Crew");
            }
            return crew;
        }

        public async Task<int> AddCrew(CrewCreate crew)
        {
            Crew newCrew = new Crew
            {
                FlightId = crew.FlightId,
                EmployeeId = crew.EmployeeId,
                PostId = crew.PostId
            };

            if (newCrew == null || !bd.Flight.Any(x => x.Id == crew.FlightId) || !bd.Employee.Any(x => x.Id == crew.EmployeeId) || !bd.Post.Any(x => x.Id == crew.PostId))
            {
                throw new Exception("Crew");
            }

            EntityEntry<Crew> ent = await crewBd.AddAsync(newCrew);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Crew");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateCrew(CrewUpdate crew)
        {
            if (crew == null || !crewBd.Any(x => x.Id == crew.Id) || !bd.Flight.Any(x => x.Id == crew.FlightId) || !bd.Employee.Any(x => x.Id == crew.EmployeeId) || !bd.Post.Any(x => x.Id == crew.PostId))
            {
                throw new Exception("Crew");
            }

            Crew upCrew = crewBd.Find(crew.Id);
            upCrew.FlightId = crew.FlightId;
            upCrew.EmployeeId = crew.EmployeeId;
            upCrew.PostId = crew.PostId;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteCrew(int id)
        {
            Crew crew = crewBd.FirstOrDefault(o => o.Id == id);
            if (crew == null)
            {
                throw new Exception("Crew");
            }

            EntityEntry<Crew> ent = crewBd.Remove(crew);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Crew");
            }
            await bd.SaveChangesAsync();
        }
    }
}
