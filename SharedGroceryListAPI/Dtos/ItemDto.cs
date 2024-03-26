namespace SharedGroceryListAPI.Dtos;

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ListItemDto> ListItems { get; set; }
    public ICollection<RecipeItemDto> RecipeItems { get; set; }
}