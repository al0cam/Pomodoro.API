# Pomodoro.API

[![.NET](https://github.com/al0cam/Pomodoro.API/actions/workflows/dotnet.yml/badge.svg)](https://github.com/al0cam/Pomodoro.API/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## 📝 Description

This repository contains the backend API for a Pomodoro Technique management application. Built with ASP.NET Core, it provides a robust RESTful interface for managing `TaskItem` entities, which represent individual tasks to be completed using the Pomodoro method.

The API supports standard CRUD (Create, Read, Update, Delete) operations for task items, allowing clients to efficiently manage their Pomodoro sessions and track task progress.

## ✨ Features

* **TaskItem Management:** Comprehensive CRUD operations for `TaskItem` entities (create, retrieve, update, delete).
* **RESTful API:** Implements standard HTTP methods (GET, POST, PUT, DELETE) for clear and intuitive resource interaction.
* **Database Integration:** Seamless persistence of task items using Entity Framework Core. Configured by default for SQLite, but easily adaptable to other databases like SQL Server or PostgreSQL.
* **API Documentation:** Auto-generated interactive API documentation using Swagger/OpenAPI for easy exploration and testing.
* **Containerization:** Includes Docker support for simplified deployment and consistent development environments.

## 🚀 Technologies Used

* **ASP.NET Core 8.0** (or a recent LTS version) - The powerful web framework for building the API.
* **C#** - The primary programming language.
* **Entity Framework Core** - An Object-Relational Mapper (ORM) for data access and database management.
* **SQLite** - The default lightweight, file-based database for development.
* **Docker** - For containerizing the application.
* **Swagger/OpenAPI** - For interactive API documentation.

## 🏁 Getting Started

These instructions will guide you through setting up and running the Pomodoro.API project on your local machine for development and testing purposes.

### Prerequisites

Before you begin, ensure you have the following installed:

* [.NET SDK 8.0](https://dotnet.microsoft.com/download) (or the version your project targets)
* [Git](https://git-scm.com/downloads)
* [Docker Desktop](https://www.docker.com/get-started) (Optional, for running with Docker)
* [httpie](https://httpie.io/) and [jq](https://stedolan.github.io/jq/) (Required if you plan to use the provided `runAll.sh` test scripts)

### Local Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/al0cam/Pomodoro.API.git](https://github.com/al0cam/Pomodoro.API.git)
    cd Pomodoro.API
    ```

2.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3.  **Database Configuration and Migrations:**
    By default, the API is configured to use **SQLite**, which stores its data in a file (`app.db` or similar) within your project's data directory.

    * **Review Connection Strings:** If you wish to use a different database (e.g., SQL Server, PostgreSQL), modify the `ConnectionStrings` section in `appsettings.json` and `appsettings.Development.json`.
    * **Apply Migrations:** Run Entity Framework Core migrations to create or update your database schema:
        ```bash
        dotnet ef database update
        ```
        *(If `dotnet ef` command is not found, install the EF Core CLI tools globally: `dotnet tool install --global dotnet-ef`)*

4.  **Run the API:**
    ```bash
    dotnet run
    ```
    The API should now be running, typically accessible at `https://localhost:5001` (HTTPS) and `http://localhost:5000` (HTTP). Check your console output for the exact URLs.

### Docker Setup (Optional)

For a containerized environment, you can use Docker:

1.  **Build the Docker image:**
    ```bash
    docker build -t pomodoro-api .
    ```

2.  **Run the Docker container:**
    ```bash
    docker run -p 5000:80 -p 5001:443 pomodoro-api
    ```
    The API will be accessible at `http://localhost:5000` (HTTP) and `https://localhost:5001` (HTTPS).

## 💡 API Endpoints

The API provides the following endpoints for managing `TaskItem` resources. You can interact with these endpoints directly or through the integrated **Swagger UI** by navigating to `/swagger` (e.g., `https://localhost:5001/swagger`) in your browser when the API is running.

| Method | Endpoint              | Description                                  | Request Body Example (for `POST`/`PUT`)                        |
| :----- | :-------------------- | :------------------------------------------- | :------------------------------------------------------------- |
| `GET`  | `/api/TaskItem`       | Retrieves a list of all available task items. | `N/A`                                                          |
| `GET`  | `/api/TaskItem/{id}`  | Retrieves a specific task item by its unique ID. | `N/A`                                                          |
| `POST` | `/api/TaskItem`       | Creates a new task item.                     | `{"title": "New Task Name", "description": "Task details", "estimatedPomodoros": 3}` |
| `PUT`  | `/api/TaskItem/{id}`  | Updates an existing task item.               | `{"id": 1, "title": "Updated Name", "isCompleted": true, "estimatedPomodoros": 3}` |
| `DELETE`| `/api/TaskItem/{id}`  | Deletes a task item by its ID.               | `N/A`                                                          |

*(Note: The exact properties in the request/response bodies will depend on your `TaskItem` model definition.)*

## 🧪 Usage and Testing

You can interact with the API using command-line tools or the interactive Swagger UI.

### Using Swagger UI

Once the API is running (either locally with `dotnet run` or via Docker), open your web browser and navigate to:
`https://localhost:5001/swagger` (or `http://localhost:5000/swagger`)

From here, you can view the API documentation, send requests, and see responses directly in your browser.

### Using the Provided Test Scripts (`runAll.sh`)

The repository includes a set of Bash scripts (`post.sh`, `getById.sh`, `put.sh`, `delete.sh`, `getAll.sh`, and `runAll.sh`) to automate a full cycle of API interactions and test the endpoints.

**Prerequisites for scripts:**
* `httpie` (a user-friendly command-line HTTP client)
* `jq` (a lightweight and flexible command-line JSON processor)

**To run the automated tests:**

1.  Ensure the API is running (e.g., in a separate terminal via `dotnet run`).
2.  Make the test scripts executable:
    ```bash
    chmod +x *.sh
    ```
3.  Execute the main test suite using Bash:
    ```bash
    bash runAll.sh
    ```
    This script will perform a sequence of API calls (Create, Get by ID, Update, Delete, Get All) and report the PASS/FAIL status for each operation.

### Example `httpie` Commands

(Assuming the API is running on `http://localhost:5000`)

* **Create a TaskItem:**
    ```bash
    http POST http://localhost:5000/api/TaskItem Title="Learn ASP.NET Core" Description="Deep dive into Web APIs for backend dev." EstimatedPomodoros:=5
    ```

* **Get all TaskItems:**
    ```bash
    http GET http://localhost:5000/api/TaskItem
    ```

* **Get TaskItem by ID (replace `1` with an actual ID from a POST response):**
    ```bash
    http GET http://localhost:5000/api/TaskItem/1
    ```

* **Update a TaskItem (replace `1` with an actual ID, update fields as needed):**
    ```bash
    http PUT http://localhost:5000/api/TaskItem/1 Title="Mastered ASP.NET Core" IsCompleted:=true CompletedPomodoros:=5 EstimatedPomodoros:=5
    ```

* **Delete a TaskItem (replace `1` with an actual ID):**
    ```bash
    http DELETE http://localhost:5000/api/TaskItem/1
    ```

## 🤝 Contributing

Contributions are welcome! If you find a bug, have a suggestion for improvement, or want to add new features, please feel free to:

1.  Fork the repository.
2.  Create a new branch (`git checkout -b feature/your-feature-name`).
3.  Make your changes.
4.  Commit your changes (`git commit -m 'feat: Add new awesome feature'`).
5.  Push to the branch (`git push origin feature/your-feature-name`).
6.  Open a Pull Request.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgements

* Inspired by the Pomodoro Technique.
* Built with the robust ASP.NET Core framework.
* Special thanks to the open-source community for tools like httpie and jq.
