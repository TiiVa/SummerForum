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

	public override async Task HandleAsync(Request req, CancellationToken ct)
	{
		var department = await repo.GetByIdAsync(req.Id);

		await SendAsync(new Response
		{
			Department = department
		}, cancellation:ct);

		
	}
}