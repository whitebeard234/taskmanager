import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskItem } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskManagerService {
  baseApiUrl: string = "https://localhost:7016";
  
  constructor(private http: HttpClient) { }

  getAllTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.baseApiUrl + '/api/taskmanager');
  }

  addTask(newTask: TaskItem): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.baseApiUrl + '/api/taskmanager', newTask);
  }

  updateTask(task: TaskItem): Observable<void> {
    return this.http.put<void>(this.baseApiUrl + '/api/taskmanager', task);
  }

  deleteTask(taskId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseApiUrl}/api/taskmanager/${taskId}`);
  }
}
