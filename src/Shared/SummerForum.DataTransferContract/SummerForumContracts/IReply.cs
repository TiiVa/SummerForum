namespace SummerForum.DataTransferContract.SummerForumContracts;

public interface IReply
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime RepliedAt { get; set; }
	public IUser RepliedBy { get; set; }
	public bool IsActive { get; set; }
}