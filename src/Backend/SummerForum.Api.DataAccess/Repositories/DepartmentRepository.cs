using SummerForum.Api.DataAccess.Entities;
using SummerForum.Api.DataAccess.RepositoryInterfaces;

namespace SummerForum.Api.DataAccess.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    public async Task<Department> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Department>> GetManyAsync(int start, int count)
    {
        throw new NotImplementedException();
    }

    public async Task AddOneAsync(Department item)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOneAsync(Department item)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOneAsync(int id)
    {
        throw new NotImplementedException();
    }
}