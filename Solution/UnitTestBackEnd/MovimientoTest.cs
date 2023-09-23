using AutoMapper;
using DataBackend;
using Moq;
using RepositoryBackEnd.Movimiento;
using ServicesBackEnd;
using ServicesBackEnd.Movimiento;
using System.ComponentModel.DataAnnotations;

namespace UnitTestBackEnd
{
    [TestFixture]
    public class MovimientoTest
    {
        private IMovimientoService _movimientoService;

        [SetUp]
        public void SetUp()
        {
            var mockMovimiento = new Mock<IMovimientoRepository>();

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
            var mapper = mapperConfig.CreateMapper();

            _movimientoService = new MovimientoService(mockMovimiento.Object, mapper);
        }

        [Test]
        public async Task RealizaTtransaccionAsync()
        {
            var movimientoViewModel = new MovimientoViewModel
            {
                NIdCuenta = null,
                DFechaMovimiento = DateTime.Now,
                NTipoMovimiento = 6,
                NValor = 50,
                LVigente = true
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _movimientoService.RealizarTransaccionAsync(movimientoViewModel);
            });

            Assert.That(ex.Message, Is.EqualTo("La cuenta es obligatoria"));
        }
    }
}