using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.Endpoints.Department.Update;

public class Handler(IDepartmentRepository repo) : Endpoint<Request, EmptyResponse>
{
	public override void Configure()
	{
		Put("/departments/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var departmentToUpdate = await repo.GetByIdAsync(req.Id);

		if (departmentToUpdate is null)
		{
			return TypedResults.NotFound();
		}

		departmentToUpdate.Description = req.Description;
		departmentToUpdate.IsActive = req.IsActive;

		await repo.UpdateOneAsync(departmentToUpdate, req.Id);
		
		return TypedResults.Ok();
	}
}
