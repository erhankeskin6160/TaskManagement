using Application.DTOs;
using Application.Repository;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TaskItem.Queries
{
    public class GetTaskItemsByStatusQuery : IRequest<List<TaskItemResponse>>
    {
        public TaskItemStatus? Status { get; set; } // Status optional, null olursa tüm task’lar gelir
    }

    public class GetTaskItemsByStatusQueryHandler : IRequestHandler<GetTaskItemsByStatusQuery, List<TaskItemResponse>>
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;

        public GetTaskItemsByStatusQueryHandler(ITaskItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TaskItemResponse>> Handle(GetTaskItemsByStatusQuery request, CancellationToken cancellationToken)
        {
            var taskItems = await _repository.GetAllAsync();

            if (request.Status.HasValue)
            {
                taskItems = taskItems.Where(t => t.Status == request.Status.Value).ToList();
            }

            return _mapper.Map<List<TaskItemResponse>>(taskItems);
        }
    }
}
