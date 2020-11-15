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
    public class EducationController : ControllerBase
    {

        private readonly Context _context;
        public EducationController(Context context)
        {
            _context = context;
        }

        [HttpGet("getEducationList")]
        public ActionResult getEducation()
        {

            var edu = _context.Education.ToList();
            return new JsonResult(edu);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<EducationDTO>> getEducationById(int Id)
        {

            var edu = await _context.Education.FindAsync(Id);
            return new JsonResult(edu);
        }
        [HttpPost("saveEdit")]
        public async Task<ActionResult<EducationDTO>> PostBusinessTravel(EducationDTO educationDTO)
        {
            if (educationDTO.Id == 0)
            {

                var education = new Education
                {
                    Name = educationDTO.Name
                };
                _context.Education.Add(education);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                try
                {
                    var dbEducation = _context.Education.Find(educationDTO.Id);

                    dbEducation.Name = educationDTO.Name;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(educationDTO.Id))
                        return NotFound();
                    else
                        throw;
                }

            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<EducationDTO>> DeleteEducation(int id)
        {
            var education = await _context.Education.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            };
            _context.Education.Remove(education);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool EducationExists(int id)
        {
            return _context.Education.Any(e => e.Id == id);
        }

    }
}
