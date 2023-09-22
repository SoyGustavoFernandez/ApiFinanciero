using Microsoft.AspNetCore.Mvc;
using DataBackend.Models;
using DataBackend;
using LoggingBackEnd;
using ServicesBackEnd.Persona;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        private const string _nombreTabla = "persona";

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
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
                string message = $"Error interno del servidor: {ex.InnerException}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
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
                    string message = $"No se encontró ningún registro en la tabla {_nombreTabla} con el ID {id}";
                    LoggerManager.LogWarning(message);
                    return NotFound(message);
                }
                return Ok(persona);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.InnerException}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
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
                    string message = $"No se encontró ningún registro en la tabla {_nombreTabla} con el ID {id}";
                    LoggerManager.LogWarning(message);
                    return NotFound(message);
                }
                LoggerManager.LogInfo($"Se actualizó correctamente el ID {id} en la tabla {_nombreTabla}");
                return NoContent();
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.InnerException}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // POST: api/Persona
        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] PersonaViewModel persona)
        {
            try
            {
                var createdPersonaId = await _personaService.CreatePersonaAsync(persona);
                LoggerManager.LogInfo($"Se insertó correctamente el registro con el ID {createdPersonaId} en la tabla {_nombreTabla}");
                return CreatedAtAction(nameof(GetPersonaById), new { id = createdPersonaId }, null);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.InnerException}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
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
                    string message = $"No se encontró ningún registro en la tabla {_nombreTabla} con el ID {id}";
                    LoggerManager.LogWarning(message);
                    return NotFound(message);
                }
                LoggerManager.LogInfo($"Se eliminó correctamente el registro con el ID {id} de la tabla {_nombreTabla}");
                return NoContent();
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.InnerException}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }
    }
}