using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Entities;

public class Post : IPost, IEntity<int>
{
	public int Id { get; set; }
	public string Description { get; set; }
	public IUser StartedBy { get; set; }
	public DateTime StartedAt { get; set; } = DateTime.UtcNow;
	public string Text { get; set; }
	public int ReadByQuantity { get; set; }
	public int NumberOfReplies { get; set; }
	public List<IReply> Replies { get; set; }
	public IDiscussion Discussion { get; set; }
	public bool IsActive { get; set; }
}