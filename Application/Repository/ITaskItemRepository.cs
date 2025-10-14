using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

    namespace Application.Repository
    {
        public interface ITaskItemRepository
        {
        Task<List<TaskItem>> GetAllAsync();

        // Belirli bir TaskItem'ı Id ile getirir, eğer yoksa null döner
        Task<TaskItem?> GetByIdAsync(Guid id);

            // Belirli bir durumdaki tüm TaskItem'ları getirir
            Task<List<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status);

            // Yeni TaskItem ekler
            Task AddAsync(TaskItem entity);

            // Var olan TaskItem'ı günceller
            Task UpdateAsync(TaskItem entity);

            // Var olan TaskItem'ı siler
            Task DeleteAsync(TaskItem entity);

            // Değişiklikleri kaydeder
            Task SaveChangesAsync();
        }
    }

 
