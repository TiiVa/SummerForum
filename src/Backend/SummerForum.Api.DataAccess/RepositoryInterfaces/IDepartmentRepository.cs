using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IDepartmentRepository : IRepository<DepartmentDto, int>
{
    // Det som är unikt för Department lägger vi här
}