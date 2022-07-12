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
    public class PassengerController : ControllerBase
    {
        private readonly ILogger<PassengerController> ilogger;
        private PassengerService passengerService;

        public PassengerController(ILogger<PassengerController> logger, PassengerService ps)
        {
            ilogger = logger;
            passengerService = ps;
        }

        /// <summary>
        /// Получить список пассажиров
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> ReadList()
        {
            return await passengerService.GetPassengerList();
        }

        /// <summary>
        /// Получить пассажира по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> Read(int id)
        {
            Passenger passenger = await passengerService.GetPassenger(id);
            return Ok(passenger);
        }

        /// <summary>
        /// Добавить пассажира
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Passenger>> Create(PassengerCreate person)
        {
            int passengerId = await passengerService.AddPassenger(person);
            return Ok($"Пассажир добавлен под Id = {passengerId}!");
        }

        /// <summary>
        /// Изменить пассажира
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Passenger>> Update(PassengerUpdate passenger)
        {
            await passengerService.UpdatePassenger(passenger);
            return Ok("Данные о пассажире обновлены!");
        }

        /// <summary>
        /// Удалить пассажира по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Passenger>> Delete(int id)
        {
            await passengerService.DeletePassenger(id);
            return Ok("Пассажир удалён!");
        }
    }
}
