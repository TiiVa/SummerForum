using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.DataTransferContract.DTOs;

public class DiscussionDto : IEntity<int>
{
	public int Id { get; set; }
	public string Description { get; set; }
	public bool IsActive { get; set; }
	public List<PostDto> Posts { get; set; }
	public DepartmentDto Department { get; set; }
}