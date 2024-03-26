namespace SharedGroceryListAPI.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Text { get; set; }

    public bool? IsFeatured { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();
}
