using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Department.GetAll;

public class Handler(IDepartmentRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/departments");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var departments = await repo.GetManyAsync(req.Start, req.Count);

		if (departments == null)
		{
			return TypedResults.NotFound();
		}

		await SendAsync(new Response()
		{
			Departments = departments
		}, cancellation: ct);

		return TypedResults.Ok();
		
	}
}
