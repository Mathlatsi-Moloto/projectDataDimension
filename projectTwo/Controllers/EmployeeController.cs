using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectDataDimension.Data;
using projectDataDimension.DTOs;
using projectDataDimension.Models;

namespace projectDataDimension.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly Context _context;
        public EmployeeController(Context context)
        {
            _context = context;
        }

        [HttpGet("get EmployeeList")]
        public ActionResult getEmployee()
        {

            var emp = _context.Employee.ToList();
            return new JsonResult(emp);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<EmployeeDTO>> getEmployeeById(int employeeNumber)
        {

            var emp = await _context.Employee.FindAsync(employeeNumber);
            return new JsonResult(emp);
        }
        [HttpPost("saveEdit")]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDTO)
        {
            if (employeeDTO.EmployeeNumber == 0)
            {

                var employee = new Employee
                {
                    Age = employeeDTO.Age,
                    Attrition = employeeDTO.Attrition,

                   

                };
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                try
                {
                    var dbEmployee = _context.Employee.Find(employeeDTO.EmployeeNumber);

                    dbEmployee.Age = employeeDTO.Age;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeDTO.EmployeeNumber))
                        return NotFound();
                    else
                        throw;
                }

            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessTravelDTO>> DeleteEmployee(int employeeNumber)
        {
            var employee = await _context.Employee.FindAsync(employeeNumber);
            if (employee == null)
            {
                return NotFound();
            };
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool EmployeeExists(int employeeNumber)
        {
            return _context.Employee.Any(e => e.EmployeeNumber == employeeNumber);
        }

    }
}
