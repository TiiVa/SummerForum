using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;
using System.ComponentModel.DataAnnotations;

namespace SummerForum.Api.DataAccess.Entities;

public class User : IUser, IEntity<int>
{
	public int Id { get; set; }
	[MaxLength(10, ErrorMessage="User name must be between 5-10 characters"), MinLength(5)]
	public string UserName { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	public string Role { get; set; }
	public virtual ICollection<Post> Posts { get; set; }
	public bool IsActive { get; set; }
}