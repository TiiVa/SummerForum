using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Entities;

public class Discussion : IDiscussion, IEntity<int>
{
	public int Id { get; set; }
	public string Description { get; set; }
	public bool IsActive { get; set; }
	public virtual List<Post>? Posts { get; set; }
	public Department? Department { get; set; }
}