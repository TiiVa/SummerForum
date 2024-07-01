namespace SummerForum.DataTransferContract.SummerForumContracts;

public interface IPost
{
	public int Id { get; set; }
	public string Description { get; set; }
	public IUser StartedBy { get; set; }
	public DateTime StartedAt { get; set; }
	public string Text { get; set; }
	public List<IReply> Replies { get; set; }
	public IDiscussion Discussion { get; set; }
	public bool IsActive { get; set; }
}