namespace SharedGroceryListAPI.Models;

public partial class ListItem
{
    public int ListId { get; set; }

    public int ItemId { get; set; }

    public string? Quantity { get; set; }

    public bool? IsActive { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual List List { get; set; } = null!;
}
