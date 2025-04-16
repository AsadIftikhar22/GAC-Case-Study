using Polly;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using WmsIntegration.Core.Entities;
using System.Xml.Serialization;


namespace WmsIntegration.Scheduler
{
    public class XmlProcessor
    {
        private readonly ILogger<XmlProcessor> _logger;
        private readonly HttpClient _httpClient;

        public XmlProcessor(ILogger<XmlProcessor> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task ProcessXmlFileAsync(string filePath)
        {
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(2), (ex, ts, ctx) =>
                {
                    _logger.LogWarning($"Retrying due to: {ex.Message}");
                });

            await retryPolicy.ExecuteAsync(async () =>
            {
                var xml = XDocument.Load(filePath);

                var tasks = new List<Task>();

                // Customers
                var customers = xml.Descendants("Customer");
                foreach (var customerElement in customers)
                {
                    var customer = DeserializeElement<Customer>(customerElement);
                    var json = JsonSerializer.Serialize(customer);
                    tasks.Add(PostToApiAsync("/api/customers", json));
                }

                // Products
                 var products = xml.Descendants("Product");
                 foreach (var productElement in products)
                 {
                     var product = DeserializeElement<Product>(productElement);
                     var json = JsonSerializer.Serialize(product);
                     tasks.Add(PostToApiAsync("/api/products", json));
                 }

                // // PurchaseOrders
                 var pos = xml.Descendants("PurchaseOrder");
                 foreach (var poElement in pos)
                 {
                     var po = DeserializeElement<PurchaseOrder>(poElement);
                     var json = JsonSerializer.Serialize(po);
                     tasks.Add(PostToApiAsync("/api/purchaseorders", json));
                 }

                // // SalesOrders
                 var sos = xml.Descendants("SalesOrder");
                 foreach (var soElement in sos)
                 {
                     var so = DeserializeElement<SalesOrder>(soElement);
                     var json = JsonSerializer.Serialize(so);
                     tasks.Add(PostToApiAsync("/api/salesorders", json));
                 }

                await Task.WhenAll(tasks);
                _logger.LogInformation($"Successfully processed all entities from {filePath}");
            });
        }
        private async Task PostToApiAsync(string endpoint, string json){
            var payload= new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            System.Console.WriteLine($"payload {json}");
            var response = await _httpClient.PostAsync(AppSettings.GetSetting("ERPURL")+endpoint,payload
            );

            response.EnsureSuccessStatusCode();
        }

        private T DeserializeElement<T>(XElement element){
                using var reader = element.CreateReader();
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader)!;
        }

    }
}
