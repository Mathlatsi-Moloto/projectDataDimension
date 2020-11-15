using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectDataDimension.Data;
using projectDataDimension.DTOs;
using projectDataDimension.Models;


namespace projectDataDimension.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentControlller : ControllerBase
    {

        private readonly Context _context;
        public DepartmentControlller(Context context)
        {
            _context = context;
        }

        [HttpGet("getDepartmentlList")]
        public ActionResult getDepartment()
        {

            var dep = _context.Department.ToList();
            return new JsonResult(dep);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<DepartmentDTO>> getDepartmentById(int Id)
        {

            var travel = await _context.Department.FindAsync(Id);
            return new JsonResult(travel);
        }
        [HttpPost("saveEdit")]
        public async Task<ActionResult<BusinessTravelDTO>> PostDepartment(DepartmentDTO departmentDTO)
        {
            if (departmentDTO.Id == 0)
            {

                var department = new Department
                {
                    Name = departmentDTO.Name
                };
                _context.Department.Add(department);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                try
                {
                    var dbDepartment = _context.Department.Find(departmentDTO.Id);

                    dbDepartment.Name = departmentDTO.Name;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(departmentDTO.Id))
                        return NotFound();
                    else
                        throw;
                }

            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentDTO>> DeleteDepartment(int id)
        {
            var depar = await _context.Department.FindAsync(id);
            if (depar == null)
            {
                return NotFound();
            };
            _context.Department.Remove(depar);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }

    }
}
