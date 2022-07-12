using BdAirport.Bd;
using BdAirport.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> ilogger;
        private FlightService flightService;

        public FlightController(ILogger<FlightController> logger, FlightService ps)
        {
            ilogger = logger;
            flightService = ps;
        }

        /// <summary>
        /// Получить список рейсов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> ReadList()
        {
            return await flightService.GetFlightList();
        }

        /// <summary>
        /// Получить рейс по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> Read(int id)
        {
            Flight Flighf = await flightService.GetFlight(id);
            return Ok(Flighf);
        }

        /// <summary>
        /// Добавить рейс
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Flight>> Create(FlightCreate flight)
        {
            int flightId = await flightService.AddFlight(flight);
            return Ok($"Рейс добавлен под Id = {flightId}!");
        }

        /// <summary>
        /// Изменить рейс
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Flight>> Update(FlightUpdate flight)
        {
            await flightService.UpdateFlight(flight);
            return Ok("Данные о рейсе обновлены!");
        }

        /// <summary>
        /// Удалить рейс по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Flight>> Delete(int id)
        {
            await flightService.DeleteFlight(id);
            return Ok("Рейс удалён!");
        }
    }
}
