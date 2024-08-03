namespace SummerForum.CommonInterfaces;

public interface IService<TEntity, TId> where TEntity : IEntity<TId>
{
	public Task<TEntity> GetByIdAsync(TId id);
	public Task<IEnumerable<TEntity>> GetManyAsync(int start, int count);
	public Task AddOneAsync(TEntity item);
	public Task UpdateOneAsync(TEntity item, int id);
	public Task DeleteOneAsync(TId id);
}