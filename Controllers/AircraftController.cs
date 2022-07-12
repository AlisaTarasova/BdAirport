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
    public class AircraftController : ControllerBase
    {
        private readonly ILogger<AircraftController> ilogger;
        private AircraftService aircraftService;

        public AircraftController(ILogger<AircraftController> logger, AircraftService ps)
        {
            ilogger = logger;
            aircraftService = ps;
        }

        /// <summary>
        /// Получить список самолётов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aircraft>>> ReadList()
        {
            return await aircraftService.GetAircraftList();
        }

        /// <summary>
        /// Получить самолёт по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Aircraft>> Read(int id)
        {
            Aircraft aircraft = await aircraftService.GetAircraft(id);
            return Ok(aircraft);
        }

        /// <summary>
        /// Добавить самолёт
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Aircraft>> Create(AircraftCreate aircraft)
        {
            int aircraftId = await aircraftService.AddAircraft(aircraft);
            return Ok($"Самолёт добавлен под Id = {aircraftId}!");
        }

        /// <summary>
        /// Изменить самолёт
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Aircraft>> Update(AircraftUpdate aircraft)
        {
            await aircraftService.UpdateAircraft(aircraft);
            return Ok("Данные о самолёте обновлены!");
        }

        /// <summary>
        /// Удалить самолёт по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Aircraft>> Delete(int id)
        {
            await aircraftService.DeleteAircraft(id);
            return Ok("Самолёт удалён!");
        }
    }
}
