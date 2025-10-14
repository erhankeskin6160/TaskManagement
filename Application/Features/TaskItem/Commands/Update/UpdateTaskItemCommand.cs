using Application.Repository;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TaskItems.Command.Update
{
    // Update komutu
    public class UpdateTaskItemCommand : IRequest<UpdateTaskItemResponse>
    {
        public Guid Id { get; set; }  // Güncellenecek TaskItem Id
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskItemStatus? Status { get; set; }
    }

    // Update komutu handler
    public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand, UpdateTaskItemResponse>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper mapper;
        public UpdateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper )
        {
            _taskItemRepository = taskItemRepository;
            this.mapper = mapper;
        }

        public async Task<UpdateTaskItemResponse>Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            // Mevcut TaskItem'ı bul
            var taskItem = await _taskItemRepository.GetByIdAsync(request.Id);

            if (taskItem == null)
                throw new KeyNotFoundException($"TaskItem with Id {request.Id} not found.");

            // Değerleri güncelle (null değilse)
            if (!string.IsNullOrEmpty(request.Title))
                taskItem.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Description))
                taskItem.Description = request.Description;

            if (request.DueDate.HasValue)
                taskItem.DueDate = request.DueDate.Value;

            if (request.Status.HasValue)
                taskItem.Status = request.Status.Value;

            // Güncelle ve kaydet
            await _taskItemRepository.UpdateAsync(taskItem);
            await _taskItemRepository.SaveChangesAsync();


            return mapper.Map<UpdateTaskItemResponse>(taskItem);

        }
    }

    
    
}
