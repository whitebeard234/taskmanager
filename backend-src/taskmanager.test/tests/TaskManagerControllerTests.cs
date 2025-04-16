using Xunit;
using Moq;
using taskmanager.api.Controllers;
using taskmanager.api.Data;
using taskmanager.api.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using taskmanager.api.Utilities;

namespace taskmanager.test.tests;

public class TaskManagerControllerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IValidator<CreateTaskItemDto>> _mockValidator;
    private readonly TaskManagerController _controller;

    public TaskManagerControllerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockValidator = new Mock<IValidator<CreateTaskItemDto>>();
        _controller = new TaskManagerController(_mockUnitOfWork.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task GetTasks_ReturnsOkResult_WithListOfTasks()
    {
        // Arrange
        var tasks = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test Task" } };
        _mockUnitOfWork.Setup(u => u.TaskManagerRepository.GetAllTasksAsync()).ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetTasks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnTasks = Assert.IsType<List<TaskItem>>(okResult.Value);
        Assert.Single(returnTasks);
    }

    [Fact]
    public async Task GetTask_ReturnsOkResult_WithTask()
    {
        // Arrange
        var task = new TaskItem { Id = 1, Title = "Test Task" };
        _mockUnitOfWork.Setup(u => u.TaskManagerRepository.GetTaskByIdAsync(1)).ReturnsAsync(task);

        // Act
        var result = await _controller.GetTask(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnTask = Assert.IsType<TaskItem>(okResult.Value);
        Assert.Equal(1, returnTask.Id);
    }

    [Fact]
    public async Task CreateTask_ReturnsCreatedAtActionResult_WithTask()
    {
        // Arrange
        var createTaskItemDto = new CreateTaskItemDto { Title = "New Task", Description = "Description", Status = Status.Pending };
        var validationResult = new ValidationResult();
        _mockValidator.Setup(v => v.ValidateAsync(createTaskItemDto, default)).ReturnsAsync(validationResult);
        _mockUnitOfWork.Setup(u => u.TaskManagerRepository.AddTaskAsync(It.IsAny<TaskItem>())).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _controller.CreateTask(createTaskItemDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnTask = Assert.IsType<TaskItem>(createdAtActionResult.Value);
        Assert.Equal("New Task", returnTask.Title);
    }


    [Fact]
    public async Task UpdateTask_ReturnsNoContentResult()
    {
        // Arrange
        var task = new TaskItem { Id = 1, Title = "Updated Task" };
        _mockUnitOfWork.Setup(u => u.TaskManagerRepository.UpdateTask(It.IsAny<TaskItem>())).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _controller.UpdateTask(task);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTask_ReturnsNoContentResult()
    {
        // Arrange
        var task = new TaskItem { Id = 1, Title = "Test Task" };
        _mockUnitOfWork.Setup(u => u.TaskManagerRepository.GetTaskByIdAsync(1)).ReturnsAsync(task);

        // Act
        var result = await _controller.DeleteTask(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
