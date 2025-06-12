# Pomodoro API - Backend for Task Management & Focus Timer

This repository contains the backend API for the Pomodoro application, designed to manage user authentication and task-related data. Built with .NET Core, it provides the necessary endpoints for the Pomodoro UI frontend to persist and retrieve user-specific information and tasks.

## Features

### User Authentication
- Secure user registration.
- User login with JSON Web Token (JWT) generation for secure access.

### Task Management
- Create, read, update, and delete (CRUD) task items.
- Tasks are associated with specific users, ensuring data privacy and personalization.
- Persistence of task details including title, description, completion status, estimated, and completed Pomodoro counts.

## Technologies Used

- **ASP.NET Core**: A cross-platform, high-performance framework for building modern, cloud-enabled, Internet-connected applications.
- **C#**: The primary programming language used for backend development.
- **Entity Framework Core**: An object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects.
- **JSON Web Tokens (JWT)**: For secure and stateless user authentication.
- **SQL Server** (or other relational database): For data storage (typically configured via Entity Framework Core).

## Project Structure

```
├── Controllers
│   ├── BasicController.cs
│   └── TaskItemController.cs
├── Data
│   └── PomodorDbContext.cs
├── Migrations
├── Models
│   ├── ApplicationUserModel.cs
│   ├── TaskItemDTO.cs
│   └── TaskItemModel.cs
├── Pomodoro.API.csproj
├── Pomodoro.API.http
├── Program.cs
├── Properties
│   └── launchSettings.json
├── Repositories
│   └── TaskItem
├── Services
│   ├── ITaskItemService.cs
│   └── TaskItemService.cs
├── data
│   └── pomodoro.db
├── biome.json
├── compose.yml
├── dockerfile
├── endpoint_tests
│   ├── TaskItem/
│   └── User/
├── LICENSE
└── Readme.md
````

## Setup and Installation

### Prerequisites

- .NET SDK (LTS version recommended)
- A database server if you are running it locally and not in docker (e.g., SQL Server, PostgreSQL, SQLite). Ensure connection strings in `appsettings.json` are updated accordingly.

### Backend Installation

```bash
# Clone the repository
git clone https://github.com/al0cam/Pomodoro.API.git
cd Pomodoro.API

# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update

# Apply database migrations from your machine if you are running it locally
dotnet ef database update --connection "Data Source=./data/pomodoro.db"

# If needed, create the initial migration
dotnet ef migrations add InitialCreate -p Pomodoro.API -s Pomodoro.API

# Run the application
dotnet run
````

The API will typically start on `http://localhost:5000/` or a similar address, as configured in `Properties/launchSettings.json`.

## API Endpoints

### Account Endpoints (`/api/Account`)

#### `POST /api/Account/register`

* **Description**: Registers a new user account.
* **Request Body**: JSON object with `email`, `password`, and `confirmPassword`.
* **Response**: Success message or validation errors.

#### `POST /api/Account/login`

* **Description**: Authenticates a user and returns a JWT.
* **Request Body**: JSON object with `email` and `password`.
* **Response**: JSON object containing `accessToken`, `tokenType`, `expiresIn`, and `refreshToken`.

### Task Item Endpoints (`/api/TaskItem`)

#### `GET /api/TaskItem`

* **Description**: Retrieves all tasks for the authenticated user.
* **Authentication**: Requires a valid JWT in the `Authorization: Bearer <token>` header.
* **Response**: Array of task objects.

#### `GET /api/TaskItem/{id}`

* **Description**: Retrieves a specific task by ID for the authenticated user.
* **Authentication**: Requires a valid JWT.
* **Response**: A single task object.

#### `POST /api/TaskItem`

* **Description**: Creates a new task for the authenticated user.
* **Authentication**: Requires a valid JWT.
* **Request Body**: JSON object containing task details (`title`, `description`, `isCompleted`, `estimatedPomodoros`, `completedPomodoros`).
* **Response**: The newly created task object including its generated `id` and `createdAt` timestamp.

#### `PUT /api/TaskItem/{id}`

* **Description**: Updates an existing task for the authenticated user.
* **Authentication**: Requires a valid JWT.
* **Request Body**: JSON object with updated task details (including the `id`).
* **Response**: No content (`204`) on success.

#### `DELETE /api/TaskItem/{id}`

* **Description**: Deletes a task for the authenticated user.
* **Authentication**: Requires a valid JWT.
* **Response**: No content (`204`) on success.

## Contributing

Contributions to the Pomodoro API are welcome. If you have suggestions for new features, improvements, or bug fixes, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
