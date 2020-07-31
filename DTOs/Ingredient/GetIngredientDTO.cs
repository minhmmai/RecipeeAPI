
namespace RecipeeAPI.DTOs.Ingredient
{
    public class GetIngredientDTO
    {
        public int Id { get; set; }
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
    }
}
