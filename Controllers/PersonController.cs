using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BdAirport.Bd;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BdAirport.Services;

namespace BdAirport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> ilogger;
        private PersonService personService;

        public PersonController(ILogger<PersonController> logger, PersonService ps)
        {
            ilogger = logger;
            personService = ps;
        }

        /// <summary>
        /// Получить список людей
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> ReadList()
        {
            return await personService.GetPersonList();
        }

        /// <summary>
        /// Получить человека по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Read(int id)
        {
            Person person = await personService.GetPerson(id);            
            return Ok(person);            
        }

        /// <summary>
        /// Добавить человека
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Person>> Create(PersonCreate person)
        {
            int personId = await personService.AddPerson(person);
            return Ok($"Человеке добавлен под Id = {personId}!");
        }

        /// <summary>
        /// Изменить человека
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Person>> Update(Person person)
        {         
            await personService.UpdatePerson(person);
            return Ok("Данные о человеке обновлены!");            
        }

        /// <summary>
        /// Удалить человека по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {       
            await personService.DeletePerson(id);
            return Ok("Человек удалён!");
        }
    }
}
