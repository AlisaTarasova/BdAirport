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
    public class AirlineController : ControllerBase
    {
        private readonly ILogger<AirlineController> ilogger;
        private AirlineService airlineService;

        public AirlineController(ILogger<AirlineController> logger, AirlineService ps)
        {
            ilogger = logger;
            airlineService = ps;
        }

        /// <summary>
        /// Получить список авиакомпаний
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airline>>> ReadList()
        {
            return await airlineService.GetAirlineList();
        }

        /// <summary>
        /// Получить авиакомпанию по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Airline>> Read(int id)
        {
            Airline airline = await airlineService.GetAirline(id);
            return Ok(airline);
        }

        /// <summary>
        /// Добавить авиакомпанию
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Airline>> Create(AirlineCreate airline)
        {
            int airlineId = await airlineService.AddAirline(airline);
            return Ok($"Авиакомпания добавлена под Id = {airlineId}!");
        }

        /// <summary>
        /// Изменить авиакомпанию
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Airline>> Update(Airline airline)
        {
            await airlineService.UpdateAirline(airline);
            return Ok("Данные о авиакомпании обновлены!");
        }

        /// <summary>
        /// Удалить авиакомпанию по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Airline>> Delete(int id)
        {
            await airlineService.DeleteAirline(id);
            return Ok("Авиакомпания удалена!");
        }
    }
}
