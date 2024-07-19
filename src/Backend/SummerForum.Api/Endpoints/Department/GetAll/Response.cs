using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Department.GetAll;

public class Response
{
	public IEnumerable<DepartmentDto> Departments { get; set; }
}