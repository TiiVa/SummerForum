using System.ComponentModel.DataAnnotations;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.DataTransferContract.DTOs;

public class UserDto : IEntity<int>
{
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false, ErrorMessage ="Username is required")]
	[StringLength(10, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 10 characters")]
	public string UserName { get; set; }
	[Required(AllowEmptyStrings = false, ErrorMessage= "Password is required")]
	public string Password { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
	public List<PostDto>? Posts { get; set; }
	public bool IsActive { get; set; }

}