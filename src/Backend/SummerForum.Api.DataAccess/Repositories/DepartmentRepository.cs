using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.Repositories;

public class DepartmentRepository(SummerForumDbContext context) : IDepartmentRepository
{
    public async Task<DepartmentDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<DepartmentDto>> IRepository<DepartmentDto, int>.GetManyAsync(int start, int count)
    {
	    throw new NotImplementedException();
    }

    public async Task AddOneAsync(DepartmentDto item)
    {
	    throw new NotImplementedException();
    }

    public async Task UpdateOneAsync(DepartmentDto item)
    {
	    throw new NotImplementedException();
    }

    

    public async Task DeleteOneAsync(int id)
    {
        throw new NotImplementedException();
    }
}