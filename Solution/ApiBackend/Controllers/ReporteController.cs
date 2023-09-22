using DataBackend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesBackEnd.Reporte;

namespace ApiBackend.Controllers
{
    [Route("api/reportes")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("clientes/{idCliente}")]
        public async Task<ActionResult<ReportesViewModel>> ObtenerEstadoDeCuenta(int idCliente, [FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var estadoDeCuenta = await _reporteService.GetEstadoCuenta(idCliente, fechaInicio, fechaFin);
                if (estadoDeCuenta == null)
                {
                    return NotFound($"No se encontró el cliente con ID {idCliente}");
                }
                return Ok(estadoDeCuenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.InnerException}");
            }
        }
    }
}
