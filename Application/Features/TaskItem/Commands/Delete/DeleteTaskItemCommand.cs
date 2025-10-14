using Application.Features.TaskItems.Command.Update;
using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TaskItems.Command.Delete
{
    // Delete komutu
    public class DeleteTaskItemCommand : IRequest<DeleteTaskItemResponse>
    {
        public Guid Id { get; set; }  // Silinecek TaskItem Id
    }

    // Delete komutu handler
    public class DeleteTaskItemCommandHandler : IRequestHandler<DeleteTaskItemCommand, DeleteTaskItemResponse>
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IMapper _mapper;
        public DeleteTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            
            _mapper = mapper;
        }

        public async Task<DeleteTaskItemResponse> Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            // Mevcut TaskItem'ı bul
            var taskItem = await _taskItemRepository.GetByIdAsync(request.Id);

            if (taskItem == null)
                throw new KeyNotFoundException($"TaskItem with Id {request.Id} not found.");

            // Sil ve kaydet
            await _taskItemRepository.DeleteAsync(taskItem);
            await _taskItemRepository.SaveChangesAsync();

            return _mapper.Map<DeleteTaskItemResponse>(taskItem);

        }
    }
}
