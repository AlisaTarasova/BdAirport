using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BdAirport.Bd;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BdAirport.Services;

namespace BdAirport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class АirportDestinationController : ControllerBase
    {
        private readonly ILogger<АirportDestinationController> ilogger;
        private АirportDestinationService airportService;

        public АirportDestinationController(ILogger<АirportDestinationController> logger, АirportDestinationService ps)
        {
            ilogger = logger;
            airportService = ps;
        }

        /// <summary>
        /// Получить список аэропортов назначения
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<АirportDestination>>> ReadList()
        {
            return await airportService.GetAirportList();
        }

        /// <summary>
        /// Получить аэропорт назначения по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<АirportDestination>> Read(int id)
        {
            АirportDestination airport = await airportService.GetAirport(id);
            return Ok(airport);
        }

        /// <summary>
        /// Добавить аэропорт назначения
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<АirportDestination>> Create(АirportDestinationCreate airport)
        {
            int airportId = await airportService.AddAirport(airport);
            return Ok($"Аэропорт назначения добавлен под Id = {airportId}!");
        }

        /// <summary>
        /// Изменить аэропорт назначения
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<АirportDestination>> Update(АirportDestination airport)
        {
            await airportService.UpdateAirport(airport);
            return Ok("Данные о аэропорте назначения обновлены!");
        }

        /// <summary>
        /// Удалить аэропорт назначения по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<АirportDestination>> Delete(int id)
        {
            await airportService.DeleteAirport(id);
            return Ok("Аэропорт назначения удалён!");
        }
    }
}
