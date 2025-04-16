using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsIntegration.Core.Entities
{
    public class Product
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            
            [Required]
            [StringLength(50)]
            public string ProductCode { get; set; } = string.Empty;
            
            [Required]
            [StringLength(200)]
            public string Title { get; set; } = string.Empty;
            
            [StringLength(500)]
            public string Description { get; set; } = string.Empty;
    }
}
