# To-Do List API

The To-Do List API is a RESTful web service that provides a simple yet effective way to manage to-do items. It allows creating, updating, marking as completed, and deleting to-do items. The API is built using .NET 8 minimal API, ensuring a lightweight and high-performance solution.

## Features

- **CRUD Operations**: Create, Read (List), Update (Mark as Completed), and Delete to-do items.
- **In-Memory Data Storage**: Fast and transient data access, suitable for testing and lightweight applications.
- **Input Validation & Error Handling**: Robust validation to ensure that the data is correct, and comprehensive error handling to improve debugging and user experience.
- **Unit Testing**: Ensures that the system is reliable and functions as expected.
- **Rate Limiting**: Protects the API from excessive use and ensures fair resource usage.
- **Authentication**: Enhances security by ensuring that only authorized users can access the API.
- **Swagger/OpenAPI**: Swagger Enabled for API docs and easy testing.

## Getting Started

These instructions will guide you through getting a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

.NET 8 Required, Docker Desktop (Optional)


### Installation

```bash
// Clone the repository
git clone https://github.com/ebrahimbharmal007/TodoAPI.git
// Navigate to the project directory
cd TodoAPI
// Build and run the tests
dotnet test
// Build and run the project
cd TodoAPI
dotnet run
```

A URL gets displayed in the logs after running the above command. Click on the URL and go to the swagger docs.
```bash
//The URl will be different for you
Now listening on: http://localhost:5188
// Naviate to: {URL}/swagger/index.html`
```

## API Usage

The APIs have been divided into two section:
- **ToDoAuth**: API Endpoints added by Microsoft Identity EF. Used for Authentication/Authorization
- **ToDoCRUD**: CRUD API Endpoints for ToDo

Follow these steps to be able ot create a token which can be used to delete a todo item. Only the delete enpoint is set as protected. 

- **Step 1**: Go to `/api/auth/register` endpoint to register your user. Use any generic email and password.
- **Step 2**: Go to `/api/auth/login` endpoint to register get your access token. Set `useCookies` to `true` in the dropdown and enter the same email and password which was used for registration. This will generate an access token and add it to your cookies. 
- **Step 3**: Ignore all the other `/api/auth/*` endpoints.