using SharedGroceryListAPI.Models;

namespace SharedGroceryListAPI.Dtos;

public class ListItemDto
{
    public Item Item { get; set; }
    public int ListId { get; set; }
    public int ItemId { get; set; }
    public string? Quantity { get; set; }
    public bool? IsActive { get; set; }
    // public ItemDto Item { get; set; }
}