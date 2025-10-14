import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  TaskItem,
  TaskItemStatus,
  CreateTaskItemCommand,
  UpdateTaskItemCommand
} from '../models/task-item.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = `${environment.apiUrl}/api/TaskItem`;

  constructor(private http: HttpClient) {}

  getTasksByStatus(status?: TaskItemStatus): Observable<TaskItem[]> {
    let params = new HttpParams();
    if (status !== undefined && status !== null) {
      params = params.set('status', status.toString());
    }
    return this.http.get<TaskItem[]>(this.apiUrl, { params });
  }

  getTaskById(id: string): Observable<TaskItem> {
    return this.http.get<TaskItem>(`${this.apiUrl}/${id}`);
  }

  createTask(command: CreateTaskItemCommand): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, command);
  }

  updateTask(id: string, command: UpdateTaskItemCommand): Observable<TaskItem> {
    return this.http.put<TaskItem>(`${this.apiUrl}/${id}`, command);
  }

  deleteTask(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
