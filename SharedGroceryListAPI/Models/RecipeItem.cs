namespace SharedGroceryListAPI.Models;

public partial class RecipeItem
{
    public int RecipeId { get; set; }

    public int ItemId { get; set; }

    public string? Quantity { get; set; }

    public bool? IsActive { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
