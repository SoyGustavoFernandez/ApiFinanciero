using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataBackend.Models;
using ServicesBackEnd;
using DataBackend;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        //private readonly RetoBackendContext _context;
        private readonly IPersonaService _personaService;

        public PersonaController(/*RetoBackendContext context*/IPersonaService personaService)
        {
            _personaService = personaService;
            //_context = context;
        }

        // GET: api/Persona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblPersona>>> GetAllPersonas()
        {
            try
            {
                var personas = await _personaService.GetAllPersonasAsync();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblPersona>> GetPersonaById(int id)
        {
            try
            {
                var persona = await _personaService.GetPersonaByIdAsync(id);
                if (persona == null)
                {
                    return NotFound($"No se encontró ninguna persona con el ID {id}");
                }
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Persona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, [FromBody] PersonaViewModel persona)
        {
            try
            {
                var updated = await _personaService.UpdatePersonaAsync(id, persona);
                if (!updated)
                {
                    return NotFound($"No se encontró ninguna persona con el ID {id}");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Persona
        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] PersonaViewModel persona)
        {
            try
            {
                var createdPersonaId = await _personaService.CreatePersonaAsync(persona);
                return CreatedAtAction(nameof(GetPersonaById), new { id = createdPersonaId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            try
            {
                var deleted = await _personaService.DeletePersonaAsync(id);
                if (!deleted)
                {
                    return NotFound($"No se encontró ninguna persona con el ID {id}");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
