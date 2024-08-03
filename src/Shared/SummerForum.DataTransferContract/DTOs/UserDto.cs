using System.ComponentModel.DataAnnotations;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.DataTransferContract.DTOs;

public class UserDto : IEntity<int>
{
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
	public string UserName { get; set; }
	[Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
	public string Password { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
	public List<PostDto> Posts { get; set; } = new();
	public bool IsActive { get; set; }

}