using System.Security.Cryptography;

namespace SummerForum.CommonInterfaces;

public interface IService<TEntity, TId> where TEntity : IEntity<TId>
{
	Task<TEntity> GetByIdAsync(TId id);
	Task<IEnumerable<TEntity>> GetManyAsync(int start, int count);
	Task AddOneAsync(TEntity item);
	Task UpdateOneAsync(TEntity item, int id);
	Task DeleteOneAsync(TId id);
}