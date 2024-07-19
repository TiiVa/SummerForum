using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.Endpoints.Department.GetById;

public class Handler(IDepartmentRepository repo) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("/departments/{id}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, NotFound>> HandleAsync(Request req, CancellationToken ct)
	{
		var department = await repo.GetByIdAsync(req.Id);

		if (department is null)
		{
			return TypedResults.NotFound();
		}

		await SendAsync(new Response
		{
			Department = department
		}, cancellation:ct);

		return TypedResults.Ok();
	}
}