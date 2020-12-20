using RecipeeAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeeAPI.Models
{
    public class Method : BaseEntity
    {
        [Required]
        public short Index { get; set; }
        [StringLength(500)]
        [Required]
        public string Detail { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
