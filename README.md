# Task Manager WebApplication

This is a Task Manager API built with .NET 8 and SPA WebApplication with Angular 19. It allows you to create, read, update, and delete tasks.

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or later
- [Node.js](https://nodejs.org/) (version 16 or later)
- [Angular CLI](https://angular.io/cli) (version 19.2.5 or later)

## API Endpoints

- **GET /api/taskmanager**: Get all tasks.
- **GET /api/taskmanager/{id}**: Get a task by ID.
- **POST /api/taskmanager**: Create a new task.
- **PUT /api/taskmanager**: Update an existing task.
- **DELETE /api/taskmanager/{id}**: Delete a task by ID.

## Backend Project Structure

- `Controllers/TaskManagerController.cs`: Contains the API endpoints.
- `Models/CreateTaskItemDto.cs`: Data transfer object for creating tasks.
- `Validator/CreateTaskItemDtoValidator.cs`: Validator for `CreateTaskItemDto`.
- `Program.cs`: Entry point of the application and configuration of services.


![TaskManager](https://github.com/user-attachments/assets/0875bfe2-5551-4ecd-8c1c-191a6a11d25f)

## License

This project is licensed under the MIT License.
