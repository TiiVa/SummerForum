namespace SummerForum.DataTransferContract.SummerForumContracts;

public interface IUser
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	public string AvatarPicturePath { get; set; }
	public List<IPost> Posts { get; set; }
	public List<IReply> Replies { get; set; }
	public DateTime RegisteredAt { get; set; }
	public bool IsActive { get; set; }

}