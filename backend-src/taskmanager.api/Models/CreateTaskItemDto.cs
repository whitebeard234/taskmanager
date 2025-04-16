using taskmanager.api.Utilities;

namespace taskmanager.api.Models
{
    public class CreateTaskItemDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
