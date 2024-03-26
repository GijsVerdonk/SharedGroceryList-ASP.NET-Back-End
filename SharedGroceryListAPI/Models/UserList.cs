namespace SharedGroceryListAPI.Models;

public partial class UserList
{
    public int UserId { get; set; }

    public int ListId { get; set; }

    public bool? IsCreator { get; set; }

    public bool? IsActive { get; set; }

    public virtual List List { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
