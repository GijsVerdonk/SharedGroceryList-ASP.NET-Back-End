using SharedGroceryListAPI.Models;
namespace SharedGroceryListAPI.Models;

public partial class Item
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();

    public virtual ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();
}
