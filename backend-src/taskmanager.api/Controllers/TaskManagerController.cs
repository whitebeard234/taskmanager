using Microsoft.AspNetCore.Mvc;
using taskmanager.api.Data;
using taskmanager.api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace taskmanager.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidator<CreateTaskItemDto> _validator;

        public TaskManagerController(IUnitOfWork unitOfWork, IValidator<CreateTaskItemDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _unitOfWork.TaskManagerRepository.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _unitOfWork.TaskManagerRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(CreateTaskItemDto createTaskItemDto)
        {
            ValidationResult result = await _validator.ValidateAsync(createTaskItemDto);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var task = new TaskItem
            {
                Title = createTaskItemDto.Title,
                Description = createTaskItemDto.Description,
                Status = createTaskItemDto.Status
            };

            await _unitOfWork.TaskManagerRepository.AddTaskAsync(task);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateTask(TaskItem task)
        {
            await _unitOfWork.TaskManagerRepository.UpdateTask(task);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _unitOfWork.TaskManagerRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _unitOfWork.TaskManagerRepository.DeleteTask(task);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
