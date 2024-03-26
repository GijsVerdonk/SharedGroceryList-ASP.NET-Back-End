namespace SharedGroceryListAPI.Models;

public partial class List
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public DateTime? CodeActiveSince { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();

    public virtual ICollection<UserList> UserLists { get; set; } = new List<UserList>();
}
