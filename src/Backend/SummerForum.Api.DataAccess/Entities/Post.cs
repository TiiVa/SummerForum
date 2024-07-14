using System.Text.Json.Serialization;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.SummerForumContracts;

namespace SummerForum.Api.DataAccess.Entities;

public class Post : IPost, IEntity<int>
{
	public int Id { get; set; }
	public string Description { get; set; }
	public User StartedBy { get; set; }
	public DateTime StartedAt { get; set; } = DateTime.UtcNow;
	public string Text { get; set; }
	public virtual ICollection<Reply> Replies { get; set; }
	public Discussion Discussion { get; set; }
	public bool IsActive { get; set; }
}