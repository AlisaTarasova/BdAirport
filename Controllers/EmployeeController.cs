using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BdAirport.Bd;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using BdAirport.Services;

namespace BdAirport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> ilogger;
        private EmployeeService passengerService;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeService ps)
        {
            ilogger = logger;
            passengerService = ps;
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> ReadList()
        {
            return await passengerService.GetEmployeeList();
        }

        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Read(int id)
        {
            Employee employee = await passengerService.GetEmployee(id);
            return Ok(employee);
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Employee>> Create(EmployeeCreate person)
        {
            int employeeId = await passengerService.AddEmployee(person);
            return Ok($"Пассажир добавлен под Id = {employeeId}!");
        }

        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Employee>> Update(EmployeeUpdate passenger)
        {
            await passengerService.UpdateEmployee(passenger);
            return Ok("Данные о пассажире обновлены!");
        }

        /// <summary>
        /// Удалить пассажира по Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            await passengerService.DeleteEmployee(id);
            return Ok("Сотрудник удалён!");
        }
    }
}
