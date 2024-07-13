using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Post.Add;

public class Request
{
	public int Id { get; set; }
	public string Description { get; set; }
	public int StartedBy { get; set; }
	public DateTime StartedAt { get; set; } = DateTime.UtcNow;
	public string Text { get; set; }
	public List<ReplyDto> Replies { get; set; } = new();
	public int Discussion { get; set; }
	public bool IsActive { get; set; }
}