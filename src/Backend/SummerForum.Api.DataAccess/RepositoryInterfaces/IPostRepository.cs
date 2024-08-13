using SummerForum.Api.DataAccess.Entities;
using SummerForum.CommonInterfaces;
using SummerForum.DataTransferContract.DTOs;

namespace SummerForum.Api.DataAccess.RepositoryInterfaces;

public interface IPostRepository : IRepository<PostDto, int>
{
	
}