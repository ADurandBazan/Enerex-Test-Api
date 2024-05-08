## Description of the "Enrex Test" REST API

This is a .NET 7 Web API that provides a simple CRUD (Create, Read, Update, Delete) interface for managing student data. The API uses Entity Framework Core (EF Core) for database operations and an in-memory database for storage.

## Key Features:

#### The API supports basic CRUD operations for managing student data, including creating new students, retrieving existing students, updating student information, and deleting students.
#### The API uses a configurable data source, allowing you to specify the path to a text file that contains the initial student data. This is set in the appsettings.json file.
#### The API has swagger documentation.

## API Endpoints:

#### POST /api/students: Allows to create a new student.
#### PUT /api/students/{id}: Allows to update a existing student by the student id.
#### DELETE /api/students/{id}: Allows to delete a student by the student id.
#### GET /api/students: Provides all existing students, allow pagination and filtering.
#### It's recommend testing the swagger endpoint using pagination to handle the large amount of data returned by this endpoint, which can otherwise block the interface.If not you can tested using Postman or another tool.

## Technologies Used:

#### Programming Language: C#
#### Web Framework: .Net7
#### Database: InMemory

