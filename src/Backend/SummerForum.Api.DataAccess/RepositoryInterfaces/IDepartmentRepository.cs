using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IDepartmentRepository : IRepository<Department, int>
{
    // Det som är unikt för Department lägger vi här
}