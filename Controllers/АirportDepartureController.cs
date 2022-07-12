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
    public class АirportDepartureController : ControllerBase
    {
        private readonly ILogger<АirportDepartureController> ilogger;
        private АirportDepartureService airportService;

        public АirportDepartureController(ILogger<АirportDepartureController> logger, АirportDepartureService ps)
        {
            ilogger = logger;
            airportService = ps;
        }

        /// <summary>
        /// Получить список аэропортов отправления
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<АirportDeparture>>> ReadList()
        {
            return await airportService.GetAirportList();
        }

        /// <summary>
        /// Получить аэропорт отправления по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<АirportDeparture>> Read(int id)
        {
            АirportDeparture airport = await airportService.GetAirport(id);
            return Ok(airport);
        }

        /// <summary>
        /// Добавить аэропорт отправления
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<АirportDeparture>> Create(АirportDepartureCreate airport)
        {
            int airportId = await airportService.AddAirport(airport);
            return Ok($"Аэропорт отправления добавлен под Id = {airportId}!");
        }

        /// <summary>
        /// Изменить аэропорт отправления
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<АirportDeparture>> Update(АirportDeparture airport)
        {
            await airportService.UpdateAirport(airport);
            return Ok("Данные о аэропорте отправления обновлены!");
        }

        /// <summary>
        /// Удалить аэропорт отправления по Id 
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<АirportDeparture>> Delete(int id)
        {
            await airportService.DeleteAirport(id);
            return Ok("Аэропорт отправления удалён!");
        }
    }
}

