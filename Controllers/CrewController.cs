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
    public class CrewController : ControllerBase
    {
        private readonly ILogger<CrewController> ilogger;
        private CrewService crewService;

        public CrewController(ILogger<CrewController> logger, CrewService ps)
        {
            ilogger = logger;
            crewService = ps;
        }

        /// <summary>
        /// Получить список экипажей
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crew>>> ReadList()
        {
            return await crewService.GetCrewList();
        }

        /// <summary>
        /// Получить экипаж по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> Read(int id)
        {
            Crew crew = await crewService.GetCrew(id);
            return Ok(crew);
        }

        /// <summary>
        /// Добавить экипаж
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Crew>> Create(CrewCreate crew)
        {
            int crewId = await crewService.AddCrew(crew);
            return Ok($"Экипаж добавлен под Id = {crewId}!");
        }

        /// <summary>
        /// Изменить экипаж
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Crew>> Update(CrewUpdate crew)
        {
            await crewService.UpdateCrew(crew);
            return Ok("Данные о экипаже обновлены!");
        }

        /// <summary>
        /// Удалить экипаж по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crew>> Delete(int id)
        {
            await crewService.DeleteCrew(id);
            return Ok("Экипаж удалён!");
        }
    }
}
