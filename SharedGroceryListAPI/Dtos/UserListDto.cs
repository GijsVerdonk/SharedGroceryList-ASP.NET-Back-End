namespace SharedGroceryListAPI.Dtos;

public class UserListDto
{
    public int UserId { get; set; }
    public int ListId { get; set; }
    public bool? IsCreator { get; set; }
    public bool? IsActive { get; set; }
    // public ListDto List { get; set; }
    // public UserDto User { get; set; }
}