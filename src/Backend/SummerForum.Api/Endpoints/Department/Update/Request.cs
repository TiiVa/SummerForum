using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Department.Update;

public class Request
{
	public int Id { get; set; }
	public string Description { get; set; }
	public bool IsActive { get; set; }
	
}