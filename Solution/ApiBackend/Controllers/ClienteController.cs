using Microsoft.AspNetCore.Mvc;
using DataBackend.Models;
using DataBackend;
using LoggingBackEnd;
using ServicesBackEnd.Cliente;

namespace ApiBackend.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private const string _nombreTabla = "cliente";

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCliente>>> GetAllClientes()
        {
            try
            {
                var clientes = await _clienteService.GetAllClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCliente>> GetClienteById(int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id);
                if (cliente == null)
                {
                    string message = $"No se encontró ningún registro en la tabla {_nombreTabla} con el ID {id}";
                    LoggerManager.LogWarning(message);
                    return NotFound(message);
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // GET: api/Cliente/ClienteByIdPersona/5
        [HttpGet("ClienteByIdPersona/{idPersona}")]
        public async Task<ActionResult<PersonaClienteViewModel>> GetClienteByIdPersona(int idPersona)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdPersonaAsync(idPersona);
                if (cliente == null)
                {
                    string message = $"No se encontró ningún registro en la tabla {_nombreTabla} con el ID de Persona {idPersona}";
                    LoggerManager.LogWarning(message);
                    return NotFound(message);
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteViewModel cliente)
        {
            try
            {
                var updated = await _clienteService.UpdateClienteAsync(id, cliente);
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
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<TblCliente>> CreateCliente([FromBody] ClienteViewModel cliente)
        {
            try
            {
                var createdClienteId = await _clienteService.CreateClienteAsync(cliente);
                LoggerManager.LogInfo($"Se insertó correctamente el registro con el ID {createdClienteId} en la tabla {_nombreTabla}");
                return CreatedAtAction(nameof(GetClienteById), new { id = createdClienteId }, null);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCliente(int id)
        {
            try
            {
                var deleted = await _clienteService.DeleteClienteAsync(id);
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
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }
    }
}
