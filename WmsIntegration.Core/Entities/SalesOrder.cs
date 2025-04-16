using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WmsIntegration.Core.Entities
{
    public class SalesOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderId { get; set; } = string.Empty;

        [Required]
        public DateTime ProcessingDate { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(500)]
        public string ShipmentAddress { get; set; } = string.Empty;
        [JsonIgnore] 
        public Customer? Customer { get; set; }
        //[JsonIgnore] 
        public List<SalesOrderItem> Items { get; set; } = new();
    }

    public class SalesOrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("SalesOrder")]
        public int SalesOrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; }
    }
}
