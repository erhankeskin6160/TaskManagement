using Domain.Enums;

namespace Application.Features.TaskItems.Command.Update
{
    public class UpdateTaskItemResponse 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public TaskItemStatus Status { get; set; }
    }
}
