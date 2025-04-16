import { Component, OnInit } from '@angular/core';
import { TaskItem } from '../../models/task.model';
import { CommonModule } from '@angular/common';
import { TaskManagerService } from '../../services/taskmanager.service';
import { FormsModule } from '@angular/forms';
import { statusMap } from '../../shared/status';

@Component({
  selector: 'app-taskmanager',
  imports: [CommonModule, FormsModule],
  templateUrl: './taskmanager.component.html',
  styleUrl: './taskmanager.component.css'
})
export class TaskmanagerComponent implements OnInit{
  taskItems: TaskItem[] = [];
  newTask: TaskItem = {
    title: '',
    description: '',
    status: 0
  }
  errorMessage: string = '';
  statuses = Object.values(statusMap);
  selectedTask: TaskItem | null = null;

  constructor(private taskManagerService: TaskManagerService) {}
  
  ngOnInit(): void {
    this.getAllTasks();
  }

  getAllTasks() {
    this.taskManagerService.getAllTasks()
    .subscribe({
      next: (tasks) =>
      {
        this.taskItems = tasks;
      }
    });
  }

  getStatusName(status: number): string {
    return statusMap[status] ?? 'Unknown Status';
  }

  selectTask(task: TaskItem): void {
    this.selectedTask = { ...task };
  }

  updateTask(): void {
    if (!this.selectedTask) {
      this.errorMessage = 'No task selected for update.';
      return;
    }
  
    const taskPayload: TaskItem = {
      ...this.selectedTask,
      status: Number(this.selectedTask.status)
    };

    this.taskManagerService.updateTask(taskPayload)
      .subscribe({
        next: () => {
          this.getAllTasks();
          this.errorMessage = '';
          this.selectedTask = null; // Clear the selection after update
        },
        error: (error) => {
          this.errorMessage = 'Failed to update the task. Please try again.';
        }
      });
  }

  addTask() {

    if (this.newTask.status !== 0) {
      this.errorMessage = 'The status must be "Pending" when creating a new task.';
      return;
    }

    const taskPayload = {
      ...this.newTask,
      status: Number(this.newTask.status)
    };
  
    this.taskManagerService.addTask(taskPayload)
      .subscribe({
        next: (task) => {
          this.getAllTasks();
          this.errorMessage = '';
        },
        error: (error) => {
          if (error.status === 400 && error.error) {
            // Parse the error response and extract validation messages
            const validationErrors = error.error;
            this.errorMessage = Object.keys(validationErrors)
              .map(key => `${key}: ${validationErrors[key].join(', ')}`)
              .join(' | ');
          } else {
            this.errorMessage = 'An unexpected error occurred.';
          }
        }
      });
  }

  deleteTask(taskId: number): void {
    if (!confirm('Are you sure you want to delete this task?')) {
      return;
    }
  
    this.taskManagerService.deleteTask(taskId)
      .subscribe({
        next: () => {
          this.getAllTasks(); // Refresh the task list after deletion
          this.errorMessage = '';
        },
        error: () => {
          this.errorMessage = 'Failed to delete the task. Please try again.';
        }
      });
  }
  
}
