using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsIntegration.Core.Entities
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderId { get; set; } = string.Empty;

        [Required]
        public DateTime ProcessingDate { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }
        public List<PurchaseOrderItem> Items { get; set; } = new();
    }

    public class PurchaseOrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Range(1, 1000)]
        public int Quantity { get; set; }
    }
}
