The project is an ASP.NET Core Web API in C# for managing dog records with sorting, pagination, validation, and rate limiting.
It uses a PostgreSQL database. Configuration is stored in:
D:\DogsHouse\DogsHouse.API\appsettings.Development.json
EF Core Code First is used for database setup.
Endpoints:
GET /ping — returns "Dogshouseservice.Version1.0.1"
GET /dogs — supports sorting and pagination
POST /dog — creates a new dog with validation
Exceeding request limits returns HTTP 429 Too Many Requests.
Unit and integration tests are provided in separate projects.

Swagger: http://localhost:5253/swagger/

GET /ping
This endpoint checks if the Dog House Service is running. It does not require any parameters. When executed, it returns a plain text response with the current service version.
Example response:
Dogshouseservice.Version1.0.1

GET /dogs
This endpoint returns a list of dogs. It supports optional query parameters for sorting and pagination.
Optional parameters:
attribute – the field used for sorting, for example “name”, “color”, “tailLength”, or “weight”.
order – the sort order, either “asc” for ascending or “desc” for descending.
pageNumber – the page number for pagination.
pageSize – the number of records to return per page.
Example request: 
curl -X 'GET' \
'http://localhost:5253/dogs?attribute=name&order=asc&pageNumber=1&pageSize=10' \
-H 'accept: */*'
Example response:
A JSON array of dogs, each object containing id, name, color, tailLength, and weight.

POST /dog
This endpoint creates a new dog record.
Request body fields:
name – the name of the dog.
color – the color of the dog.
tailLength – the length of the dog’s tail.
weight – the weight of the dog.
Example JSON request body:
{ "name": "TestDog", "color": "Black", "tailLength": 10, "weight": 20 }
Example response:
{ "message": "Dog created successfully" }
