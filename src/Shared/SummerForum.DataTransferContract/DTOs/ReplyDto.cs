using SummerForum.CommonInterfaces;

namespace SummerForum.DataTransferContract.DTOs;

public class ReplyDto : IEntity<int>
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime RepliedAt { get; set; } = DateTime.UtcNow;
	public int RepliedBy { get; set; }
	public int Post { get; set; }
	public bool IsActive { get; set; }
}