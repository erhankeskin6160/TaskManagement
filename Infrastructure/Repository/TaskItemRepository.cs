using Application.Repository;
 using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext _context;

        public TaskItemRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Task veri tabanına ekler ve kaydeder
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(TaskItem entity)
        {
            await _context.TaskItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Taski siler entity göre
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TaskItem entity)
        {
            _context.TaskItems.Remove(entity);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Taskleri getirir
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        /// <summary>
        /// ID göre Taski getirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Filtrelenen statuye göre taskleri listeler
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<List<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status)
        {
            return await _context.TaskItems
                                 .Where(t => t.Status == status)
                                 .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Taski Günceller
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TaskItem entity)
        {
            _context.TaskItems.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
 