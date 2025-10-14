import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { TaskItem, TaskItemStatus } from '../../models/task-item.model';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  tasks: TaskItem[] = [];
  filteredTasks: TaskItem[] = [];
  selectedStatus: string = 'all';
  searchTerm: string = '';
  isLoading: boolean = false;
  showModal: boolean = false;//
  editingTask: TaskItem | null = null;// Düzeltilecek task item
  errorMessages: string[] = []; //Hata mesajları
  TaskItemStatus = TaskItemStatus;

  //Form yapımız
  taskForm = {
    title: '',
    description: '',
    dueDate: '',
    status: TaskItemStatus.Pending
  };

  constructor(private taskService: TaskService) {}
 //Angular yüklememe başladıktan sonra  yüklenecek veriyi buraya aktrıyoruz
  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(status?: TaskItemStatus): void {
    this.isLoading = true;
    this.taskService.getTasksByStatus(status).subscribe({
      next: (data) => {
        this.tasks = data;
        this.applyFilters();
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Görevler yüklenirken hata:', error);
        this.isLoading = false;
      }
    });
  }

  applyFilters(): void {
    let filtered = [...this.tasks];

    if (this.selectedStatus !== 'all') {
      filtered = filtered.filter(task =>
        task.status === parseInt(this.selectedStatus)
      );
    }

    if (this.searchTerm) {
      const search = this.searchTerm.toLowerCase();
      filtered = filtered.filter(task =>
        task.title.toLowerCase().includes(search) ||
        (task.description && task.description.toLowerCase().includes(search))
      );
    }

    this.filteredTasks = filtered;
  }

  onStatusFilterChange(event: any): void {
    this.selectedStatus = event.target.value;
    this.applyFilters();
  }

  onSearchChange(): void {
    this.applyFilters();
  }

  openCreateModal(): void {
    this.editingTask = null;
    this.taskForm = {
      title: '',
      description: '',
      dueDate: '',
      status: TaskItemStatus.Pending
    };
    this.errorMessages = [];
    this.showModal = true;
  }

  openEditModal(task: TaskItem): void {
    this.editingTask = task;
    this.taskForm = {
      title: task.title,
      description: task.description || '',
      dueDate: new Date(task.dueDate).toISOString().split('T')[0],
      status: task.status
    };
    this.errorMessages = [];
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.editingTask = null;
  }

  saveTask(): void {
    if (!this.taskForm.title || !this.taskForm.dueDate) {
      alert('Lütfen tüm zorunlu alanları doldurun');
      return;
    }

    this.errorMessages = [];

    const command = {
      title: this.taskForm.title,
      description: this.taskForm.description,
      dueDate: new Date(this.taskForm.dueDate),
      status: this.taskForm.status
    };

    if (this.editingTask) {
      const updateCommand = { ...command, id: this.editingTask.id };
      this.taskService.updateTask(this.editingTask.id, updateCommand).subscribe({
        next: () => {
          this.loadTasks();
          this.closeModal();
        },
        error: (error) =>
          {
                this.handleApiError(error)
          }
      });
    } else {
      this.taskService.createTask(command).subscribe({
        next: () => {
          this.loadTasks();
          this.closeModal();
        },
        error: (error) => {
          this.handleApiError(error)


        }
      });
    }
  }

  deleteTask(id: string): void {
    if (confirm('Bu görevi silmek istediğinizden emin misiniz?')) {
      this.taskService.deleteTask(id).subscribe({
        next: () => this.loadTasks(),
        error: (error) => console.error('Silme hatası:', error)
      });
    }
  }

  updateTaskStatus(task: TaskItem, event: any): void {
    const newStatus = parseInt(event.target.value);
    const command = {
      id: task.id,
      title: task.title,
      description: task.description,
      dueDate: task.dueDate,
      status: newStatus
    };

    this.taskService.updateTask(task.id, command).subscribe({
      next: () => this.loadTasks(),
      error: (error) => console.error('Durum güncelleme hatası:', error)
    });
  }

  getStatusText(status: TaskItemStatus): string {
    switch (status) {
      case TaskItemStatus.Pending: return 'Beklemede';
      case TaskItemStatus.InProgress: return 'Devam Ediyor';
      case TaskItemStatus.Completed: return 'Tamamlandı';
      default: return '';
    }
  }

  getStatusClass(status: TaskItemStatus): string {
    switch (status) {
      case TaskItemStatus.Pending: return 'status-pending';
      case TaskItemStatus.InProgress: return 'status-in-progress';
      case TaskItemStatus.Completed: return 'status-completed';
      default: return '';
    }
  }
 //Bekleyen   task sayısını döndürür

  get pendingCount(): number {
    return this.tasks.filter(t => t.status === TaskItemStatus.Pending).length;
  }
 //Devam Eden task sayısını döndürür

  get inProgressCount(): number {
    return this.tasks.filter(t => t.status === TaskItemStatus.InProgress).length;
  }
 //Tamamlanan task sayısını döndürür
  get completedCount(): number {
    return this.tasks.filter(t => t.status === TaskItemStatus.Completed).length;
  }

  ///Api hatalarını getirir
   handleApiError(error: any): void {
  this.errorMessages = [];  //HAtaları temizledik

  if (error?.error?.errors && typeof error.error.errors === 'object') {
    const validationErrors = error.error.errors;

    for (const key in validationErrors) {
      if (validationErrors.hasOwnProperty(key)) {
        const messages = validationErrors[key];
        messages.forEach((msg: string) => {
          this.errorMessages.push(msg);
        });
      }
    }


  }



  else {
    alert('Beklenmeyen bir hata oluştu.');
  }
}
}
