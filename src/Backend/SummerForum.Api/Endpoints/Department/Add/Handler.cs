using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Department.Add;

public class Handler : Endpoint<Request, Results<Ok, BadRequest>>
{
	private readonly UnitOfWork _unitOfWork;
	
	public Handler(UnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public override void Configure()
	{
		Post("/departments");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var departmentToAdd = new DepartmentDto()
		{
			Description = req.Description,
		};

		var departments = await _unitOfWork.DepartmentRepository.GetManyAsync(0, 10);

		if(departments.Any(d => d.Description.Equals(req.Description)))
		{
			return TypedResults.BadRequest();

		}
		return TypedResults.Ok();
	}
}