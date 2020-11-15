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
    public class JobRoleController : ControllerBase
    {

        private readonly Context _context;
        public JobRoleController(Context context)
        {
            _context = context;
        }
        
    


   
    [HttpGet("getJobRoleList")]
    public ActionResult getJobRole()
    {

        var role = _context.JobRole.ToList();
        return new JsonResult(role);
    }
    [HttpGet("getlistbyId{id}")]
    public async Task<ActionResult<BusinessTravelDTO>> getJobRoleById(int Id)
    {

        var role = await _context.JobRole.FindAsync(Id);
        return new JsonResult(role);
    }
    [HttpPost("saveEdit")]
    public async Task<ActionResult<JobRoleDTO>> PostJobRole(JobRoleDTO jobroleDTO)
    {
        if (jobroleDTO.Id == 0)
        {

            var jobrole = new JobRole
            {
                Name = jobroleDTO.Name
            };
            _context.JobRole.Add(jobrole);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        else
        {
            try
            {
                var dbJobRole = _context.JobRole.Find(jobroleDTO.Id);

                dbJobRole.Name = jobroleDTO.Name;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobRoleExists(jobroleDTO.Id))
                    return NotFound();
                else
                    throw;
            }

        }
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<JobRoleDTO>> DeleteJobRole(int id)
    {
        var jobRole = await _context.JobRole.FindAsync(id);
        if (jobRole == null)
        {
            return NotFound();
        };
        _context.JobRole.Remove(jobRole);
        await _context.SaveChangesAsync();
        return NoContent();

    }

    private bool JobRoleExists(int id)
    {
        return _context.JobRole.Any(e => e.Id == id);
    }

}
}
