using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Entities;

public class Discussion : IDiscussion, IEntity<int>
{
	public int Id { get; set; }
	public string Description { get; set; }
	public bool IsActive { get; set; }
	public List<Post> Posts { get; set; }
	public virtual Department Department { get; set; }
}