using RecipeeAPI.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeeAPI.Models
{
    [Table("Ingredient")]
    public class Ingredient : BaseEntity
    {
        [Column(TypeName = ("decimal(5,2)"))]
        public decimal? Quantity { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
