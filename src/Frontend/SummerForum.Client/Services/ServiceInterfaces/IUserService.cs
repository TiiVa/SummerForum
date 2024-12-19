using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services.ServiceInterfaces;

public interface IUserService : IService<UserDto, int>
{
	
}