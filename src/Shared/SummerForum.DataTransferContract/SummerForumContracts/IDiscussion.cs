namespace SummerForum.DataTransferContract.SummerForumContracts;

public interface IDiscussion
{
	public int Id { get; set; }
	public string Description { get; set; }
	public bool IsActive { get; set; }
	public List<IPost> Posts { get; set; }
	public IDepartment Department { get; set; }
}