namespace RecipeeAPI.DTOs.Ingredient
{
    public class AddIngredientDTO
    {
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
    }
}