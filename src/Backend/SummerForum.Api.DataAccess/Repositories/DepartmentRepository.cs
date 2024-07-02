using Microsoft.EntityFrameworkCore;
using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.Repositories;

public class DepartmentRepository(SummerForumDbContext context) : IDepartmentRepository
{
	public async Task<DepartmentDto> GetByIdAsync(int id)
	{
		var department = await context.Departments.FindAsync(id);

		if (department is null)
		{
			return new DepartmentDto();
		}

		var departmentToReturn = new DepartmentDto
		{
			Id = department.Id,
			Description = department.Description,
			Discussions = await context.Discussions.Include(p => p.Posts)
				.ThenInclude(r => r.Replies)
				.Where(d => d.Department.Id == department.Id)
				.Select(d => new DiscussionDto
				{
					Id = d.Id,
					Description = d.Description,
					IsActive = d.IsActive,
					Posts = d.Posts.Select(p => new PostDto
					{
						Id = p.Id,
						Text = p.Text,
						StartedAt = p.StartedAt,
						StartedBy = new UserDto
						{
							Id = p.StartedBy.Id,
							UserName = p.StartedBy.UserName,
							Email = p.StartedBy.Email,
							Password = p.StartedBy.Password,
							IsActive = p.StartedBy.IsActive
						},
						Replies = p.Replies.Select(r => new ReplyDto
						{
							Id = r.Id,
							Text = r.Text,
							RepliedAt = r.RepliedAt,
							IsActive = r.IsActive
						}).ToList()
					}).ToList()
				}).ToListAsync()
		};

		return departmentToReturn;
	}

	public async Task<IEnumerable<DepartmentDto>> GetManyAsync(int start, int count)
	{
		var departments = await context.Departments
			.Include(d => d.Discussions)
			.ThenInclude(p => p.Posts)
			.ThenInclude(r => r.Replies)
			.Skip(start)
			.Take(count)
			.ToListAsync();

		var departmentsToReturn = departments.Select(d => new DepartmentDto
		{
			Id = d.Id,
			Description = d.Description,
			Discussions = d.Discussions.Select(p => new DiscussionDto
			{
				Id = p.Id,
				Description = p.Description,
				IsActive = p.IsActive,
				Posts = p.Posts.Select(r => new PostDto
				{
					Id = r.Id,
					Text = r.Text,
					StartedAt = r.StartedAt,
					StartedBy = new UserDto
					{
						Id = r.StartedBy.Id,
						UserName = r.StartedBy.UserName,
						Email = r.StartedBy.Email,
						Password = r.StartedBy.Password,
						IsActive = r.StartedBy.IsActive
					},
					Replies = r.Replies.Select(r => new ReplyDto
					{
						Id = r.Id,
						Text = r.Text,
						RepliedAt = r.RepliedAt,
						IsActive = r.IsActive
					}).ToList()
				}).ToList()
			}).ToList()
		}).ToList();

		return departmentsToReturn;
	}

	public async Task AddOneAsync(DepartmentDto item)
	{
		var department = new Department
		{
			Description = item.Description
		};

		await context.Departments.AddAsync(department);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(DepartmentDto item)
	{
		var oldDepartment = await context.Departments.FindAsync(item.Id);
		
		if(oldDepartment is null)
		{
			return;
		}

		oldDepartment.Description = item.Description;
		await context.SaveChangesAsync();

	}

	public async Task DeleteOneAsync(int id)
	{
		var departmentToDelete = await context.Departments.FindAsync(id);

		if(departmentToDelete is null)
		{
			return;
		}

		context.Departments.Remove(departmentToDelete);
		await context.SaveChangesAsync();
	}
}