namespace SharedGroceryListAPI.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Sub { get; set; }
    public string Nickname { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public ICollection<UserListDto> UserLists { get; set; }
}
