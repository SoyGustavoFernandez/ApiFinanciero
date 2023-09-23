using Newtonsoft.Json;
using System.Net;
using DataBackend;

namespace IntegrationTestBackEnd
{
    [TestFixture]
    public class ApiTest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7224")
            };
        }

        [Test]
        public void TestApiIntegration()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Personas/1");

            var response = _httpClient.SendAsync(request).Result;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseBody = response.Content.ReadAsStringAsync().Result;
            var responseObject = JsonConvert.DeserializeObject<PersonaViewModel>(responseBody);

            Assert.IsNotNull(responseObject);
            Assert.That(responseObject.NIdPersona, Is.EqualTo(1));
        }
    }
}