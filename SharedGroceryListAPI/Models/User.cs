namespace SharedGroceryListAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Sub { get; set; }

    public string? Nickname { get; set; }

    public bool? IsAdmin { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<UserList> UserLists { get; set; } = new List<UserList>();
}
