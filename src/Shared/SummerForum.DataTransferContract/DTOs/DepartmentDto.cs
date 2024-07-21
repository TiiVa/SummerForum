using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.DataTransferContract.DTOs;

public class DepartmentDto : IEntity<int>
{
	public int Id { get; set; }
	public string Description { get; set; }
	public List<DiscussionDto> Discussions { get; set; }
	public bool IsActive { get; set; }
}