namespace Application.Features.TaskItems.Command.Delete
{
    // Delete response
    public class DeleteTaskItemResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
