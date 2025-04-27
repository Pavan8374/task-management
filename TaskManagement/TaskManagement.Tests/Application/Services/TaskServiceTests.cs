using Moq;
using TaskManagement.Application.Common;
using TaskManagement.Application.DTOs.Tasks;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Interfaces;
using DomainTask = TaskManagement.Domain.Entities.Task;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;
using User = TaskManagement.Domain.Entities.User;

namespace TaskManagement.Tests.Application.Services
{
    [TestFixture]
    public class TaskServiceTests
    {
        private Mock<ITaskRepository> _mockTaskRepository;
        private Mock<IUserContext> _mockUserContext;
        private Mock<IUserService> _mockUserService;
        private TaskService _taskService;

        [SetUp]
        public void Setup()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _mockUserContext = new Mock<IUserContext>();
            _mockUserService = new Mock<IUserService>();

            _taskService = new TaskService(
                _mockTaskRepository.Object,
                _mockUserContext.Object,
                _mockUserService.Object
            );

            // Setup default user context
            _mockUserContext.Setup(x => x.UserId).Returns(1);
        }

        #region AddTask Tests

        [Test]
        public async Task AddTask_ValidRequest_ReturnsTaskDto()
        {
            // Arrange
            var createRequest = new CreateTaskRequest
            {
                Title = "Test Task",
                Description = "Test Description",
                AssignedToUserId = 2
            };

            _mockUserService.Setup(x => x.IsUserExist(It.IsAny<int>()))
                .ReturnsAsync(true);

            var createdTask = new DomainTask
            {
                Id = 1,
                Title = createRequest.Title,
                Description = createRequest.Description,
                AssignedToUserId = createRequest.AssignedToUserId,
                AssignedByUserId = _mockUserContext.Object.UserId,
                TaskStatus = TaskStatus.Todo
            };

            var taskWithUsers = new DomainTask
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                AssignedToUserId = createdTask.AssignedToUserId,
                AssignedByUserId = createdTask.AssignedByUserId,
                TaskStatus = createdTask.TaskStatus,
                AssignedByUser = new User
                {
                    Id = 1,
                    FullName = "Admin User",
                    Email = "admin@example.com"
                },
                AssignedToUser = new User
                {
                    Id = 2,
                    FullName = "Test User",
                    Email = "test@example.com"
                }
            };

            // Setup the repository to capture the task entity when AddAsync is called
            _mockTaskRepository.Setup(x => x.AddAsync(It.IsAny<DomainTask>()))
                .Callback<DomainTask>(task => task.Id = 1)
                .ReturnsAsync(createdTask);

            _mockTaskRepository.Setup(x => x.GetTask(It.IsAny<int>()))
                .ReturnsAsync(taskWithUsers);

            // Act
            var result = await _taskService.AddTask(createRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(createRequest.Title));
            Assert.That(result.Description, Is.EqualTo(createRequest.Description));
            Assert.That(result.AssignedToUser.UserId, Is.EqualTo(createRequest.AssignedToUserId));
            Assert.That(result.AssignedByUser.UserId, Is.EqualTo(_mockUserContext.Object.UserId));
            Assert.That(result.TaskStatus.Status, Is.EqualTo("Todo"));

            _mockTaskRepository.Verify(x => x.AddAsync(It.IsAny<DomainTask>()), Times.Once);
            _mockTaskRepository.Verify(x => x.GetTask(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void AddTask_NullRequest_ThrowsBusinessException()
        {
            // Arrange
            CreateTaskRequest createRequest = null;

            // Act & Assert
            var exception = Assert.ThrowsAsync<BusinessException>(async () =>
                await _taskService.AddTask(createRequest));

            Assert.That(exception.Message, Is.EqualTo("Request body is empty."));
            _mockTaskRepository.Verify(x => x.AddAsync(It.IsAny<DomainTask>()), Times.Never);
        }

        [Test]
        public void AddTask_InvalidAssignedUser_ThrowsBusinessException()
        {
            // Arrange
            var createRequest = new CreateTaskRequest
            {
                Title = "Test Task",
                Description = "Test Description",
                AssignedToUserId = 999 // Non-existent user ID
            };

            _mockUserService.Setup(x => x.IsUserExist(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act & Assert
            var exception = Assert.ThrowsAsync<BusinessException>(async () =>
                await _taskService.AddTask(createRequest));

            Assert.That(exception.Message, Is.EqualTo("The assigned user not found!"));
            _mockTaskRepository.Verify(x => x.AddAsync(It.IsAny<DomainTask>()), Times.Never);
        }

        #endregion

        #region GetAllTasks Tests

        [Test]
        public async Task GetAllTasks_TasksExist_ReturnsTaskDtoList()
        {
            // Arrange
            var tasks = new List<DomainTask>
            {
                new DomainTask
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description 1",
                    TaskStatus = TaskStatus.Todo,
                    AssignedByUserId = 1,
                    AssignedToUserId = 2,
                    AssignedByUser = new User { Id = 1, FullName = "Admin User", Email = "admin@example.com" },
                    AssignedToUser = new User { Id = 2, FullName = "Test User", Email = "test@example.com" }
                },
                new DomainTask
                {
                    Id = 2,
                    Title = "Task 2",
                    Description = "Description 2",
                    TaskStatus = TaskStatus.InProgress,
                    AssignedByUserId = 1,
                    AssignedToUserId = 3,
                    AssignedByUser = new User { Id = 1, FullName = "Admin User", Email = "admin@example.com" },
                    AssignedToUser = new User { Id = 3, FullName = "Another User", Email = "another@example.com" }
                }
            };

            _mockTaskRepository.Setup(x => x.GetAllTasks())
                .ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasks();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));

            // Check first task
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Task 1"));
            Assert.That(result[0].Description, Is.EqualTo("Description 1"));
            Assert.That(result[0].TaskStatus.Status, Is.EqualTo("Todo"));
            Assert.That(result[0].AssignedByUser.UserId, Is.EqualTo(1));
            Assert.That(result[0].AssignedToUser.UserId, Is.EqualTo(2));

            // Check second task
            Assert.That(result[1].Id, Is.EqualTo(2));
            Assert.That(result[1].Title, Is.EqualTo("Task 2"));
            Assert.That(result[1].Description, Is.EqualTo("Description 2"));
            Assert.That(result[1].TaskStatus.Status, Is.EqualTo("InProgress"));
            Assert.That(result[1].AssignedByUser.UserId, Is.EqualTo(1));
            Assert.That(result[1].AssignedToUser.UserId, Is.EqualTo(3));

            _mockTaskRepository.Verify(x => x.GetAllTasks(), Times.Once);
        }

        [Test]
        public async Task GetAllTasks_NoTasks_ReturnsEmptyList()
        {
            // Arrange
            var tasks = new List<DomainTask>();

            _mockTaskRepository.Setup(x => x.GetAllTasks())
                .ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasks();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));

            _mockTaskRepository.Verify(x => x.GetAllTasks(), Times.Once);
        }

        #endregion

        #region GetTask Tests

        [Test]
        public async Task GetTask_TaskExists_ReturnsTaskDto()
        {
            // Arrange
            int taskId = 1;
            var task = new DomainTask
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Description",
                TaskStatus = TaskStatus.InProgress,
                AssignedByUserId = 1,
                AssignedToUserId = 2,
                AssignedByUser = new User { Id = 1, FullName = "Admin User", Email = "admin@example.com" },
                AssignedToUser = new User { Id = 2, FullName = "Test User", Email = "test@example.com" }
            };

            _mockTaskRepository.Setup(x => x.GetTask(taskId))
                .ReturnsAsync(task);

            // Act
            var result = await _taskService.GetTask(taskId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(taskId));
            Assert.That(result.Title, Is.EqualTo("Test Task"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.TaskStatus.Status, Is.EqualTo("InProgress"));
            Assert.That(result.AssignedByUser.UserId, Is.EqualTo(1));
            Assert.That(result.AssignedByUser.FullName, Is.EqualTo("Admin User"));
            Assert.That(result.AssignedByUser.Email, Is.EqualTo("admin@example.com"));
            Assert.That(result.AssignedToUser.UserId, Is.EqualTo(2));
            Assert.That(result.AssignedToUser.FullName, Is.EqualTo("Test User"));
            Assert.That(result.AssignedToUser.Email, Is.EqualTo("test@example.com"));

            _mockTaskRepository.Verify(x => x.GetTask(taskId), Times.Once);
        }

        [Test]
        public void GetTask_TaskDoesNotExist_ThrowsException()
        {
            // Arrange
            int taskId = 999;

            _mockTaskRepository.Setup(x => x.GetTask(taskId))
                .ThrowsAsync(new Exception("Task not found"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _taskService.GetTask(taskId));

            _mockTaskRepository.Verify(x => x.GetTask(taskId), Times.Once);
        }

        #endregion

        #region GetAllTasksAssignedToUser Tests

        [Test]
        public async Task GetAllTasksAssignedToUser_TasksExist_ReturnsTaskDtoList()
        {
            // Arrange
            int userId = 2;
            var tasks = new List<DomainTask>
            {
                new DomainTask
                {
                    Id = 1,
                    Title = "Task 1",
                    Description = "Description 1",
                    TaskStatus = TaskStatus.Todo,
                    AssignedByUserId = 1,
                    AssignedToUserId = userId,
                    AssignedByUser = new User { Id = 1, FullName = "Admin User", Email = "admin@example.com" },
                    AssignedToUser = new User { Id = userId, FullName = "Test User", Email = "test@example.com" }
                },
                new DomainTask
                {
                    Id = 3,
                    Title = "Task 3",
                    Description = "Description 3",
                    TaskStatus = TaskStatus.Complete,
                    AssignedByUserId = 3,
                    AssignedToUserId = userId,
                    AssignedByUser = new User { Id = 3, FullName = "Another Admin", Email = "another@example.com" },
                    AssignedToUser = new User { Id = userId, FullName = "Test User", Email = "test@example.com" }
                }
            };

            _mockTaskRepository.Setup(x => x.GetAllTasksAssignedToUser(userId))
                .ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasksAssignedToUser(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));

            // Check all tasks are assigned to the correct user
            foreach (var task in result)
            {
                Assert.That(task.AssignedToUser.UserId, Is.EqualTo(userId));
                Assert.That(task.AssignedToUser.FullName, Is.EqualTo("Test User"));
                Assert.That(task.AssignedToUser.Email, Is.EqualTo("test@example.com"));
            }

            // Check first task details
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Task 1"));
            Assert.That(result[0].TaskStatus.Status, Is.EqualTo("Todo"));
            Assert.That(result[0].AssignedByUser.UserId, Is.EqualTo(1));

            // Check second task details
            Assert.That(result[1].Id, Is.EqualTo(3));
            Assert.That(result[1].Title, Is.EqualTo("Task 3"));
            Assert.That(result[1].TaskStatus.Status, Is.EqualTo("Complete"));
            Assert.That(result[1].AssignedByUser.UserId, Is.EqualTo(3));

            _mockTaskRepository.Verify(x => x.GetAllTasksAssignedToUser(userId), Times.Once);
        }

        [Test]
        public async Task GetAllTasksAssignedToUser_NoTasks_ReturnsEmptyList()
        {
            // Arrange
            int userId = 999;
            var tasks = new List<DomainTask>();

            _mockTaskRepository.Setup(x => x.GetAllTasksAssignedToUser(userId))
                .ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasksAssignedToUser(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));

            _mockTaskRepository.Verify(x => x.GetAllTasksAssignedToUser(userId), Times.Once);
        }

        #endregion
    }
}