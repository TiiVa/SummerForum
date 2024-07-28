using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IUserRepository : IRepository<UserDto, int>
{
	Task<UserDto> GetByNameAsync(string name);
}