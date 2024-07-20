using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Reply.Add;

public class Request
{
	public int Id { get; set; }
	public string Text { get; set; }
	public DateTime RepliedAt { get; set; } = DateTime.UtcNow;
	public int RepliedBy { get; set; }
	public int Post { get; set; }
	public bool IsActive { get; set; }
}