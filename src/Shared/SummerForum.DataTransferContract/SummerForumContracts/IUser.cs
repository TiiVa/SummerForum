namespace SummerForum.DataTransferContract.SummerForumContracts;

public interface IUser
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
	public string Email { get; set; }
	

}