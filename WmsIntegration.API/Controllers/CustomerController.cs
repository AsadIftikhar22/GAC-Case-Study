using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsIntegration.Core.Entities;
using WmsIntegration.Infrastructure.Data;

namespace WmsIntegration.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            try{
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            }
            catch(Exception ex){
            Console.WriteLine($"Error is {ex} Exception Message is {ex.Message} Exception Stacktrace is {ex.StackTrace}");
            }
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Customers.ToListAsync());
    }

    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try{
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            }
            catch(Exception ex){
            Console.WriteLine($"Error is {ex} Exception Message is {ex.Message} Exception Stacktrace is {ex.StackTrace}");
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Products.ToListAsync());
    }

    [ApiController]
    [Route("api/purchaseorders")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseOrderController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PurchaseOrder po)
        {
            try{
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.PurchaseOrders.Add(po);
            await _context.SaveChangesAsync();
            }
            catch(Exception ex){
            Console.WriteLine($"Error is {ex} Exception Message is {ex.Message} Exception Stacktrace is {ex.StackTrace}");
            }
            return Ok(po);
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.PurchaseOrders
            .Include(p => p.Customer)
            .Include(p => p.Items).ThenInclude(i => i.Product)
            .ToListAsync());
    }

    [ApiController]
    [Route("api/salesorders")]
    public class SalesOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesOrderController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SalesOrder so)
        {
           try{
            if (!ModelState.IsValid){
                 // Log the model state errors
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                Console.WriteLine("ModelState is invalid. Errors:");
                foreach (var error in errors){
                    Console.WriteLine($"- {error}");
                    }
                    return BadRequest(ModelState);
                }
            _context.SalesOrders.Add(so);
            await _context.SaveChangesAsync();
            }
            catch(Exception ex){
            Console.WriteLine($"Error is {ex} Exception Message is {ex.Message} Exception Stacktrace is {ex.StackTrace}");
            }
            return Ok(so);
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.SalesOrders
            .Include(s => s.Customer)
            .Include(s => s.Items).ThenInclude(i => i.Product)
            .ToListAsync());
    }
}
