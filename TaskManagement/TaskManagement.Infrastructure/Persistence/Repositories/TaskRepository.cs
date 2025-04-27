using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Interfaces;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : BaseRepository<Task, AppDbContext>, ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id">Task identity</param>
        /// <returns>Task</returns>

        //Fixed this method by using async, await keywords, since Task is asynchrounous operation
        //Added try,catch block to catch exceptions 
        public async Task<Task> GetTask(int id)
        {
            try
            {
                return await GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //can even use logger like serilog or nlog to track exceptions
                throw;
            }
        }

        /// <summary>
        /// Get all Tasks 
        /// </summary>
        /// <returns>Tasks list</returns>
        
        //Fixed this method by using async, await keywords, since Task is asynchrounous operation
        //Added try,catch block to catch exceptions 
        public async Task<List<Task>> GetAllTasks()
        {
            try
            {
                return await GetQuery().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all tasks assigned to user
        /// </summary>
        /// <param name="userId">Assigned user identity</param>
        /// <returns>Task list</returns>
        public async Task<List<Task>> GetAllTasksAssignedToUser(int userId)
        {
            return await GetQuery().Where(x => x.AssignedToUserId == userId).ToListAsync();
        }

        protected override IQueryable<Task> GetQuery()
        {
            return _context.Tasks.AsNoTracking()
                .Include(x => x.AssignedToUser).Include(x => x.AssignedByUser).Where(x => x.IsActive);
        } 
    }
}
