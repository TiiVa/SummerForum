namespace SummerForum.DataTransferContract.SummerForumContracts;

public interface IDepartment
{
	public int Id { get; set; }
	public string Description { get; set; }
	public List<IDiscussion> Discussions { get; set; }

}