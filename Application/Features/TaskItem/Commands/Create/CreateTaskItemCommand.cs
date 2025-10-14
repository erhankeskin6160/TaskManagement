using Application.Repository;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using TaskItemEntity = Domain.Entities.TaskItem;


namespace Application.Features.TaskItems.Command.Create
{
    public class CreateTaskItemCommand : IRequest<CreateTaskItemResponse>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    }

    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, CreateTaskItemResponse>
    {
        private readonly ITaskItemRepository taskItemRepository;
        private readonly IMapper _mapper;

        public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            this.taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<CreateTaskItemResponse> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {

            var taskItem = new TaskItemEntity
            {
                Id = Guid.NewGuid(), // Her zaman benzersiz
                Title = request.Title,
                Description = request.Description,
                CreatedDate = request.CreatedDate,
                DueDate = request.DueDate,
                Status = request.Status
            };

            await taskItemRepository.AddAsync(taskItem);
            await taskItemRepository.SaveChangesAsync();

            return _mapper.Map<CreateTaskItemResponse>(taskItem);


        }

    }
}
