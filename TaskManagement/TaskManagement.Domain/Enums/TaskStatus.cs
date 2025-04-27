namespace TaskManagement.Domain.Enums
{
    /// <summary>
    /// Task status
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// Task Todo
        /// </summary>
        Todo = 0,

        /// <summary>
        /// Task In progress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Task on hold
        /// </summary>
        Hold  = 2,

        /// <summary>
        /// Task completed
        /// </summary>
        Complete = 3,

        /// <summary>
        /// Task Cancelled
        /// </summary>
        Cancelled = 4
    }
}
