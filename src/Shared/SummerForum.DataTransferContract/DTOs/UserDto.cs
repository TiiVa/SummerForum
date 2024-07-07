using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.DataTransferContract.DTOs;

public class UserDto : IEntity<int>
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	public List<PostDto> Posts { get; set; } = new();// tagit bort List<Replies>
	public bool IsActive { get; set; }
}