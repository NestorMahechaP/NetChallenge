using NetChallenge.Database.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetChallenge.Database.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Description field is required.")]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        [StringRange(AllowableValues = new[] { "Good", "Vehicle", "Land", "Apartment" })]
        public string Type { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Only positive values allowed.")]
        public double Value { get; set; }
        [Required(ErrorMessage = "Purchase Date field is required.")]
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }
        [Required]
        [StringRange(AllowableValues = new[] { "Active", "Inactive" })]
        public string Status { get; set; }
    }
}
