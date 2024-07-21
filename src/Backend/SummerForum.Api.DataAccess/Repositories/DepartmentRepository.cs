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
		var department = await context.Departments.Where(d => d.IsActive == true).FirstOrDefaultAsync(d => d.Id == id);

		if (department is null)
		{
			return new DepartmentDto();
		}

		var departmentToReturn = new DepartmentDto
		{
			Id = department.Id,
			Description = department.Description,
			IsActive = department.IsActive,
			Discussions = await context.Discussions.Include(p => p.Posts).Where(d => d.Department.Id == department.Id)
				.Select(d => new DiscussionDto
				{
					Id = d.Id,
					Description = d.Description,
					IsActive = d.IsActive,
					//Posts = d.Posts.Select(p => new PostDto
					//{
					//	Id = p.Id,
					//	Text = p.Text,
					//	StartedAt = p.StartedAt,
					//	StartedBy = p.StartedBy.Id,
					//	Replies = p.Replies.Select(r => new ReplyDto
					//	{
					//		Id = r.Id,
					//		Text = r.Text,
					//		RepliedAt = r.RepliedAt,
					//		IsActive = r.IsActive
					//	}).ToList()
					//}).ToList()
				}).ToListAsync()
		};

		return departmentToReturn;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="start"></param>
	/// <param name="count"></param>
	/// <returns>Collection of departments in specified range</returns>

	public async Task<IEnumerable<DepartmentDto>> GetManyAsync(int start, int count)
	{
		var departments = await context.Departments
			.Where(d => d.IsActive == true)
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
			IsActive = d.IsActive,
			Discussions = d.Discussions.Select(p => new DiscussionDto
			{
				Id = p.Id,
				Description = p.Description,
				IsActive = p.IsActive,
				//Posts = p.Posts.Select(r => new PostDto
				//{
				//	Id = r.Id,
				//	Text = r.Text,
				//	StartedAt = r.StartedAt,
				//	StartedBy = r.StartedBy.Id,
				//	Replies = r.Replies.Select(r => new ReplyDto
				//	{
				//		Id = r.Id,
				//		Text = r.Text,
				//		RepliedAt = r.RepliedAt,
				//		IsActive = r.IsActive
				//	}).ToList()
				//}).ToList()
			}).ToList()
		}).ToList();

		return departmentsToReturn;
	}

	public async Task AddOneAsync(DepartmentDto item)
	{
		var departmentExists = await context.Departments.AnyAsync(d => d.Description.Equals(item.Description));

		if(departmentExists)
		{
			return;
		}

		var department = new Department
		{
			Description = item.Description,
			IsActive = true

		};

		await context.Departments.AddAsync(department);
		await context.SaveChangesAsync();
	}

	public async Task UpdateOneAsync(DepartmentDto item, int id)
	{
		var oldDepartment = await context.Departments.FindAsync(id);
		
		if(oldDepartment is null)
		{
			return;
		}

		oldDepartment.Description = item.Description;
		oldDepartment.IsActive = item.IsActive;

		await context.SaveChangesAsync();

	}

	public async Task DeleteOneAsync(int id)
	{
		var departmentToDelete = await context.Departments.FindAsync(id);

		if(departmentToDelete is null)
		{
			return;
		}

		var entityEntry = context.Departments.Update(departmentToDelete);
		entityEntry.Property(d => d.IsActive).CurrentValue = false;

		await context.SaveChangesAsync();
	}
}