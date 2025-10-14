using Domain.Enums;

namespace Application.DTOs
{
    public class TaskItemUpdateDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskItemStatus? Status { get; set; }
    }
}
