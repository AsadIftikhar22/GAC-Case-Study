using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsIntegration.Core.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

         [Required]
        public CustomerType CustomerCategory { get; set; }
    }

       public enum CustomerType
    {
        Business,  // For business customers
        Individual // For individual customers
    }
}
