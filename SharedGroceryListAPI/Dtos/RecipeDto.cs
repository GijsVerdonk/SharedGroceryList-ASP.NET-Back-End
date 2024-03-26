namespace SharedGroceryListAPI.Dtos;

public class RecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; }
    public ICollection<RecipeItemDto> RecipeItems { get; set; }
}
