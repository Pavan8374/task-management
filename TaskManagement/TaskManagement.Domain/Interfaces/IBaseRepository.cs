using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// Get TEntity
        /// </summary>
        /// <returns>TEntities</returns>
        public Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Get TEntity by id 
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Add TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Update TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> ChangeAsync(TEntity entity);

        /// <summary>
        /// Delete TEntity
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns>TEntity</returns>
        public Task<TEntity> RemoveAsync(TEntity entity);
    }
}
