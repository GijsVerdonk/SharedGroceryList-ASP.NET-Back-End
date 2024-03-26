namespace SharedGroceryListAPI.Dtos;

public class ListDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime CodeActiveSince { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ListItemDto> ListItems { get; set; }
    public ICollection<UserListDto> UserLists { get; set; }
}