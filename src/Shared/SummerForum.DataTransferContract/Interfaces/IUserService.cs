using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Client.Services;

public interface IUserService : IService<UserDto, int>
{
}