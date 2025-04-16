using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WmsIntegration.Tests
{
    public class ApiControllerTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient { BaseAddress = new System.Uri("http://localhost:5077/") };
        }

        [Test]
        public async Task CreateCustomer_Should_Return_OK_When_Valid_Data()
        {
            //Where 0 is Individual and 1 is Business
            var customer = new { Id = 3, Name = "Tech Corp", CustomerCategory = 0 };
            
            var jsonContent = JsonConvert.SerializeObject(customer);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/customers", content);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateProduct_Should_Return_OK_When_Valid_Data()
        {
            var product = new { Id = 1, ProductCode = "GAC--001", Title = "GAC Product A", Description = "GAC Description A" };
            var jsonContent = JsonConvert.SerializeObject(product);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/products", content);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreatePurchaseOrder_Should_Return_OK_When_Valid_Data()
        {
            var purchaseOrder =new {OrderId = "PO1", ProcessingDate = DateTime.UtcNow, CustomerId = 1 };
           
            var jsonContent = JsonConvert.SerializeObject(purchaseOrder);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/purchaseorders", content);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateSalesOrder_Should_Return_OK_When_Valid_Data()
        {
            var SalesOrder=new { Id = 1, OrderId = "SO1", ProcessingDate = DateTime.UtcNow, CustomerId = 1,ShipmentAddress="Testing Shipment Address" };
            var jsonContent = JsonConvert.SerializeObject(SalesOrder);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/salesorders", content);

            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetCustomer_Should_Return_OK_When_Valid_Request()
        {
            var response = await _client.GetAsync("api/customers");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetProduct_Should_Return_OK_When_Valid_Request()
        {
            var response = await _client.GetAsync("api/products");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetPurchaseOrder_Should_Return_OK_When_Valid_Request()
        {
            var response = await _client.GetAsync("api/purchaseorders");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetSalesOrder_Should_Return_OK_When_Valid_Request()
        {
            var response = await _client.GetAsync("api/salesorders");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
