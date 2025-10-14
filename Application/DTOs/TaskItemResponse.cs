using Domain.Enums;

namespace Application.DTOs
{
    public class TaskItemResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public TaskItemStatus Status { get; set; }
    }
}
