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

	public override async Task HandleAsync(Request req, CancellationToken ct)
	{
		var departments = await repo.GetManyAsync();

		await SendAsync(new Response()
		{
			Departments = departments
		}, cancellation: ct);

	}
}
