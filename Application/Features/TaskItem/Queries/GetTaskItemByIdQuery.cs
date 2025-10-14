using Application.DTOs;
using Application.Repository;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskItem.Queries
{
    public class GetTaskItemByIdQuery:IRequest<TaskItemResponse>
    {
       public Guid Id { get; set; }
    }

    public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItemResponse>
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;

        public GetTaskItemByIdQueryHandler(ITaskItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        
        public async Task<TaskItemResponse> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            var taskıtem = await _repository.GetByIdAsync(request.Id);
            if (taskıtem == null)
                throw new KeyNotFoundException($"Task ıd {request.Id} bulunamadı");
            return _mapper.Map<TaskItemResponse>(taskıtem);
        }
    }
}
