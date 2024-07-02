using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Entities;

public class Reply : IReply, IEntity<int>
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime RepliedAt { get; set; } = DateTime.UtcNow;
	public User RepliedBy { get; set; }
	public bool IsActive { get; set; }
}