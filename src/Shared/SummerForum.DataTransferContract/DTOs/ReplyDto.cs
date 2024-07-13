using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.DataTransferContract.DTOs;

public class ReplyDto : IEntity<int>
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime RepliedAt { get; set; } = DateTime.UtcNow;
	public UserDto RepliedBy { get; set; }
	public PostDto BelongsToPost { get; set; }
	public bool IsActive { get; set; }
}