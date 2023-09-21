using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataBackend.Models;
using RepositoryBackEnd;
using LoggingBackEnd;
using ServicesBackEnd;
using DataBackend;

namespace ApiBackend.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        private const string _nombreTabla = "cuenta";

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        // GET: api/Cuenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCuentum>>> GetAllCuentas()
        {
            try
            {
                var cuentas = await _cuentaService.GetAllCuentasAsync();
                return Ok(cuentas);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // GET: api/Cuenta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCuentum>> GetCuentaById(int id)
        {
            try
            {
                var cliente = await _cuentaService.GetCuentaByIdAsync(id);
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

        // GET: api/Cliente/CuentaByIdCliente/5
        [HttpGet("CuentaByIdCliente/{idCliente}")]
        public async Task<ActionResult<ClienteViewModel>> GetCuentaByIdCliente(int idCliente)
        {
            try
            {
                var cliente = await _cuentaService.GetCuentaByIdClienteAsync(idCliente);
                if (cliente == null)
                {
                    string message = $"No se encontró ningún registro en la tabla {_nombreTabla} con el ID de Cliente {idCliente}";
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


        // PUT: api/Cuenta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuenta(int id, [FromBody] CuentaViewModel cuenta)
        {
            try
            {
                var updated = await _cuentaService.UpdateCuentaAsync(id, cuenta);
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

        // POST: api/Cuenta
        [HttpPost]
        public async Task<ActionResult<TblCuentum>> CreateCuenta([FromBody] CuentaViewModel cuenta)
        {
            try
            {
                var createdClienteId = await _cuentaService.CreateCuentaAsync(cuenta);
                LoggerManager.LogInfo($"Se insertó correctamente el registro con el ID {createdClienteId} en la tabla {_nombreTabla}");
                return CreatedAtAction(nameof(GetCuentaById), new
                {
                    id = createdClienteId
                }, null);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.Message}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // DELETE: api/Cuenta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCuentum(int id)
        {
            try
            {
                var deleted = await _cuentaService.DeleteCuentaAsync(id);
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
