using Domain.Enums;

namespace Application.Features.TaskItems.Command.Create
{
    public class CreateTaskItemResponse 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskItemStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
