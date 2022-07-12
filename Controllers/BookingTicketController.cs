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
    public class BookingTicketController : ControllerBase
    {
        private readonly ILogger<BookingTicketController> ilogger;
        private BookingTicketService bookingTicketService;

        public BookingTicketController(ILogger<BookingTicketController> logger, BookingTicketService ps)
        {
            ilogger = logger;
            bookingTicketService = ps;
        }

        /// <summary>
        /// Получить список забронированных билетов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTicket>>> ReadList()
        {
            return await bookingTicketService.GetBookingTicketList();
        }

        /// <summary>
        /// Получить бронь билета по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingTicket>> Read(int id)
        {
            BookingTicket bookingTicket = await bookingTicketService.GetBookingTicket(id);
            return Ok(bookingTicket);
        }

        /// <summary>
        /// Добавить бронь билета
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookingTicket>> Create(BookingTicketCreate bookingTicket)
        {
            int bookingTicketId = await bookingTicketService.AddBookingTicket(bookingTicket);
            return Ok($"Бронь билета добавлена под Id = {bookingTicketId}!");
        }

        /// <summary>
        /// Изменить бронь билета
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<BookingTicket>> Update(BookingTicketUpdate bookingTicket)
        {
            await bookingTicketService.UpdateBookingTicket(bookingTicket);
            return Ok("Данные о брони билета обновлены!");
        }

        /// <summary>
        /// Удалить бронь билета по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookingTicket>> Delete(int id)
        {
            await bookingTicketService.DeleteBookingTicket(id);
            return Ok("Бронь билета удалена!");
        }
    }
}
