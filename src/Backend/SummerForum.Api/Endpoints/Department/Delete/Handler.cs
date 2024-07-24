using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Department.Delete;

public class Handler(IDepartmentRepository repo) : Endpoint<Request, Results<Ok, NotFound>>
{
	public override void Configure()
	{
		Delete("/departments/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var departmentToDelete = await repo.GetByIdAsync(req.Id);

		if (departmentToDelete == null)
		{
			return TypedResults.NotFound();
		}

		await repo.DeleteOneAsync(req.Id);

		return TypedResults.Ok();
	}
}
