using AutoMapper;
using Domain.Entities;
using Application.DTOs;
using Application.Features.TaskItems.Command.Create;
using Application.Features.TaskItems.Command.Update;
using Application.Features.TaskItems.Command.Delete;

namespace Application.Mappings.TaskItemProfile
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
            CreateMap<CreateTaskItemCommand, TaskItem>();
            CreateMap<UpdateTaskItemCommand, TaskItem>();
            CreateMap<DeleteTaskItemCommand, TaskItem>();


            CreateMap<TaskItem, CreateTaskItemResponse>();
            CreateMap<TaskItem, UpdateTaskItemResponse>();
            CreateMap<TaskItem, DeleteTaskItemResponse>();


            //Query 

            CreateMap<TaskItem, TaskItemResponse>();

        }
    }
}
