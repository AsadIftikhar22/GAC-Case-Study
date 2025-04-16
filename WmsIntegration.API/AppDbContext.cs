using Microsoft.EntityFrameworkCore;
using WmsIntegration.Core.Entities;

namespace WmsIntegration.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<SalesOrderItem> SalesOrderItems => Set<SalesOrderItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
                
            modelBuilder.Entity<PurchaseOrderItem>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<SalesOrderItem>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId);

            // Seeding data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "GAC Customer A", CustomerCategory = CustomerType.Business},
                new Customer { Id = 2, Name = "John Doe", CustomerCategory = CustomerType.Individual },
                new Customer { Id = 3, Name = "Tech Corp", CustomerCategory = CustomerType.Business }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductCode = "GAC--001", Title = "GAC Product A", Description = "GAC Description A" },
                new Product { Id = 2, ProductCode = "GAC--002", Title = "GAC Product B", Description = "GAC Description B" }
            );

            modelBuilder.Entity<PurchaseOrder>().HasData(
                new PurchaseOrder { Id = 1, OrderId = "PO1", ProcessingDate = DateTime.UtcNow, CustomerId = 1 },
                new PurchaseOrder { Id = 2, OrderId = "PO2", ProcessingDate = DateTime.UtcNow.AddDays(-1), CustomerId = 2 }
            );

            modelBuilder.Entity<PurchaseOrderItem>().HasData(
                new PurchaseOrderItem { Id = 1, ProductId = 1, PurchaseOrderId = 1, Quantity = 10 },
                new PurchaseOrderItem { Id = 2, ProductId = 2, PurchaseOrderId = 2, Quantity = 5 }
            );

            modelBuilder.Entity<SalesOrder>().HasData(
                new SalesOrder { Id = 1, OrderId = "SO1", ProcessingDate = DateTime.UtcNow, CustomerId = 1 },
                new SalesOrder { Id = 2, OrderId = "SO2", ProcessingDate = DateTime.UtcNow.AddDays(-1), CustomerId = 2 }
            );

            modelBuilder.Entity<SalesOrderItem>().HasData(
                new SalesOrderItem { Id = 1, ProductId = 1, SalesOrderId = 1, Quantity = 3 },
                new SalesOrderItem { Id = 2, ProductId = 2, SalesOrderId = 2, Quantity = 7 }
            );
        }
    }
}