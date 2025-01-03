using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using SummerForum.Api.DataAccess;

namespace SummerForum.Api.Endpoints.Discussion.GetAllByDepartment;

public class Handler(UnitOfWork unitOfWork) : Endpoint<Request, Response>
{
	public override void Configure()
	{
		Get("discussions/departments/{departmentId}");
		AllowAnonymous();
	}

	public override async Task<Results<Ok, BadRequest>> HandleAsync(Request req, CancellationToken ct)
	{
		var discussions = await unitOfWork.DiscussionRepository.GetAllByDepartment(req.DepartmentId);


		await SendAsync(new Response()
		{
			DiscussionsByDepartment = discussions
		}, cancellation: ct);

		return TypedResults.Ok();
	}
}