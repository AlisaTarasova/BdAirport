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
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> ilogger;
        private TicketService ticketService;

        public TicketController(ILogger<TicketController> logger, TicketService ps)
        {
            ilogger = logger;
            ticketService = ps;
        }

        /// <summary>
        /// Получить список билетов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> ReadList()
        {
            return await ticketService.GetTicketList();
        }

        /// <summary>
        /// Получить билет по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> Read(int id)
        {
            Ticket ticket = await ticketService.GetTicket(id);
            return Ok(ticket);
        }

        /// <summary>
        /// Добавить билет
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Ticket>> Create(TicketCreate ticket)
        {
            int ticketId = await ticketService.AddTicket(ticket);
            return Ok($"Билет добавлен под Id = {ticketId}!");
        }

        /// <summary>
        /// Изменить билет
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Ticket>> Update(TicketUpdate ticket)
        {
            await ticketService.UpdateTicket(ticket);
            return Ok("Данные о билете обновлены!");
        }

        /// <summary>
        /// Удалить билет по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticket>> Delete(int id)
        {
            await ticketService.DeleteTicket(id);
            return Ok("Билет удалён!");
        }
    }
}
