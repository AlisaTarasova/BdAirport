using BdAirport.Bd;
using BdAirport.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport.Services
{
    public class EmployeeService
    {
        ApplicationContext bd;
        private readonly ILogger<EmployeeController> logger;

        DbSet<Employee> employeeBd { get => bd.Employee; }


        public EmployeeService(ILogger<EmployeeController> l, ApplicationContext context)
        {
            logger = l;
            bd = context;
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeList()
        {
            return await bd.Employee.Include(p => p.Person).AsNoTracking().ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            Employee employee = await employeeBd.Include(p => p.Person).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                throw new Exception("Employee");
            }
            return employee;
        }

        public async Task<int> AddEmployee(EmployeeCreate employee)
        {
            Employee newEmployee = new Employee
            {
                PersonId = employee.PersonId,
                Experience = employee.Experience,
            };

            if (newEmployee == null || employee.Experience < 0 || !bd.Person.Any(x => x.Id == employee.PersonId))
            {
                throw new Exception("Employee");
            }

            EntityEntry<Employee> ent = await employeeBd.AddAsync(newEmployee);
            EntityState st = ent.State;

            if (st != EntityState.Added)
            {
                throw new Exception("Employee");
            }
            await bd.SaveChangesAsync();
            return ent.Entity.Id;
        }

        public async Task UpdateEmployee(EmployeeUpdate employee)
        {
            if (employee == null || !employeeBd.Any(x => x.Id == employee.Id) || employee.Experience < 0 || !bd.Person.Any(x => x.Id == employee.PersonId))
            {
                throw new Exception("Employee");
            }

            Employee upEmployee = employeeBd.Find(employee.Id);
            upEmployee.PersonId = employee.PersonId;
            upEmployee.Experience = employee.Experience;

            await bd.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            Employee employee = employeeBd.FirstOrDefault(o => o.Id == id);
            if (employee == null)
            {
                throw new Exception("Employee");
            }

            EntityEntry<Employee> ent = employeeBd.Remove(employee);
            EntityState st = ent.State;

            if (st != EntityState.Deleted)
            {
                throw new Exception("Employee");
            }
            await bd.SaveChangesAsync();
        }
    }
}
