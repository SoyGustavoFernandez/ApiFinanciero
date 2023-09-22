using Microsoft.AspNetCore.Mvc;
using DataBackend.Models;
using ServicesBackEnd.Movimiento;
using LoggingBackEnd;
using DataBackend;
using Newtonsoft.Json;

namespace ApiBackend.Controllers
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;
        private const string _nombreTabla = "movimientos";

        public MovimientoController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        // GET: api/Movimiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblMovimiento>> GetMovimientoById(int id)
        {
            try
            {
                var cliente = await _movimientoService.GetMovimientoByIdAsync(id);
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
                string message = $"Error interno del servidor: {ex.InnerException}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // GET: api/Movimiento/MovimientoByIdCuenta/5
        [HttpGet("Cuentas/{idCuenta}")]
        public async Task<ActionResult<ClienteViewModel>> GetMovimientoByIdCuenta(int idCuenta)
        {
            try
            {
                var cliente = await _movimientoService.GetMovimientoByIdCuentaAsync(idCuenta);
                if (cliente == null)
                {
                    string message = $"No se encontró ningún registro en la tabla {_nombreTabla} con el ID de Cuenta {idCuenta}";
                    LoggerManager.LogWarning(message);
                    return NotFound(message);
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                string message = $"Error interno del servidor: {ex.InnerException}";
                LoggerManager.LogError(message);
                return StatusCode(500, message);
            }
        }

        // POST: api/Movimiento/RealizarTransaccion
        [HttpPost("RealizarTransaccion")]
        public async Task<IActionResult> RealizarTransaccion([FromBody] MovimientoViewModel movimiento)
        {
            try
            {
                var mensaje = await _movimientoService.RealizarTransaccionAsync(movimiento);
                LoggerManager.LogInfo($"{mensaje}: {JsonConvert.SerializeObject(movimiento)}");
                return Ok(mensaje);
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