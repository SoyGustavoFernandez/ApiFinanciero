using DataBackend;
using ServicesBackEnd.Persona;
using System.ComponentModel.DataAnnotations;

namespace UnitTestBackEnd
{
    [TestFixture]
    public class PersonaTests
    {
        private IPersonaService _personaService;

        [SetUp]
        public void SetUp()
        {
            _personaService = new PersonaService();
        }

        [Test]
        public void CreatePersona_ValidData()
        {
            var objPersona = new PersonaViewModel
            {
                SNombres = "Gustavo Fernández",
                NEdad = 26,
                CDireccion = "Piura - Perú",
                CIdentificacion = "70886499",
                CTelefono = "51941917926",
                NGenero = 1
            };

            PersonaViewModel createdPersona = _personaService.CreatePersonaTest(objPersona);

            Assert.IsNotNull(createdPersona);
        }

        [Test]
        public void CreatePersona_InvalidData()
        {
            var objPersona = new PersonaViewModel
            {
                SNombres = null,                    //Obligatorio
                NEdad = 15,                         //Edad mayor a 18 y menor a 75 años
                CDireccion = "Piura - Perú",
                CIdentificacion = "70886499",
                CTelefono = "51941917926",
                NGenero = 6                          // 1: Masculino - 2: Femenino
            };

            Assert.Throws<ValidationException>(() => _personaService.CreatePersonaTest(objPersona));
        }
    }
}