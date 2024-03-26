namespace SharedGroceryListAPI.Dtos;

public class ListItemDto
{
    public int ListId { get; set; }
    public int ItemId { get; set; }
    public string? Quantity { get; set; }
    public bool? IsActive { get; set; }
    // public ItemDto Item { get; set; }
}