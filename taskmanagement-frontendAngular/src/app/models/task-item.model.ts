export enum TaskItemStatus {
  Pending = 0,
  InProgress = 1,
  Completed = 2
}

export interface TaskItem {
  id: string;
  title: string;
  description?: string;
  createdDate: Date;
  dueDate: Date;
  status: TaskItemStatus;
}

export interface CreateTaskItemCommand {
  title: string;
  description?: string;
  dueDate: Date;
  status: TaskItemStatus;
}

export interface UpdateTaskItemCommand {
  id: string;
  title: string;
  description?: string;
  dueDate: Date;
  status: TaskItemStatus;
}
