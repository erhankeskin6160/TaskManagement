using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; } // Task Item Id değeri
        public string Title { get; set; } = string.Empty;// Taskın Başlığı

        public string Description { get; set; } = string.Empty;//Taskın Açıklaması 500 karakteri geçmiyecek fludent validation yapılack sonra

        public DateTime CreatedDate { get; set; } //Oluşturulma Tarihi

        public DateTime DueDate { get; set; } // Taskin Bitmesi Gereken Tarih FludentValidation'la oluşturulma tarihi ile bitiş tarihi aynı olmaz kuralını koyacağız sonra

        public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;//Taskın durumu

    }
}
