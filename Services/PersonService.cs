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
    public class PersonService
    {
        ApplicationContext bd;
        private readonly ILogger<PersonController> logger;

        DbSet<Person> personBd { get => bd.Person; }
        

        public PersonService(ILogger<PersonController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Person>>> GetPersonList()
        {
            return await personBd.AsNoTracking().ToListAsync();
        }

        public async Task<Person> GetPerson(int id)
        {
            Person person = await personBd.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                throw new Exception("Person");
            }
            return person;
        }       

        public async Task<int> AddPerson(PersonCreate person)
        {
            Person newPerson = new Person
            {
                Surname = person.Surname,
                Name = person.Name,
                Patronymic = person.Patronymic,
                Age = person.Age,
                Passport = person.Passport
            };

            if (newPerson == null || person.Name == null || person.Surname == null || person.Patronymic == null || person.Age < 0 || person.Passport == null)
            {
                throw new Exception("Person");
            }

            EntityEntry<Person> ent = await personBd.AddAsync(newPerson);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Person");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdatePerson(Person person)
        {
            if (person == null || !personBd.Any(x => x.Id == person.Id) || person.Name == null || person.Surname == null || person.Age < 0  || person.Patronymic == null || person.Passport == null)
            {
                throw new Exception("Person");
            }

            Person upPerson = personBd.Find(person.Id);
            upPerson.Surname = person.Surname;
            upPerson.Name = person.Name;
            upPerson.Patronymic = person.Patronymic;
            upPerson.Age = person.Age;
            upPerson.Passport = person.Passport;

            await bd.SaveChangesAsync();
        }

        public async Task DeletePerson(int id)
        {
            Person person = personBd.FirstOrDefault(o => o.Id == id);
            if (person == null)
            {
                throw new Exception("Person");
            }

            EntityEntry<Person> ent = personBd.Remove(person);
            EntityState st = ent.State;            

            if (st != EntityState.Deleted)
            {
                throw new Exception("Person");
            }
            await bd.SaveChangesAsync();
        }       
    }
}
