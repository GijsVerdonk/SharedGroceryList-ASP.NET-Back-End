namespace SharedGroceryListAPI.Dtos;

public class RecipeItemDto
{
    public int RecipeId { get; set; }
    public int ItemId { get; set; }
    public string Quantity { get; set; }
    public bool IsActive { get; set; }
    public string Item { get; set; }
    public RecipeDto Recipe { get; set; }
}