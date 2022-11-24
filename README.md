# Snapsoft API
## Algorithm task

The algorithm's input is an n-long integer array, and the output is also an n-long integer
array, where the element with index i is equal to the product of the input array except the
element i. 
For example, with input of [1, 2, 3, 4] the output should be [24, 12, 8, 6], due to:
24=2*3*4, 12=1*3*4, 8=1*2*4, 6=1*2*3.

## Installation and run
- [Postgre Sql] is required to run this application. You have to have a user named: postgres with a password: postgres to be able to run and use this application properly.
- Open the project
- [EF Core] is responsible for the database setup and migration. Therefore, you should have it installed on your machine.
- After completing the previous steps, you should run: ``` 'dotnet ef database update' ``` from the Package Manager Console
- You are able to run the application, and use it

## Usage

You can contact with the API on the following address

```sh
https://localhost:3000
```

## Endpoints

Dillinger is currently extended with the following plugins.
Instructions on how to use them in your own application are linked below.

| METHOD | URL | Parameters | Example
| ------ | ------ | ------ | ------ |
| POST | https://localhost:3000/api/Calculate/a | [BODY] | {  "array": [10,20,30],  "comment": "string"} |
| POST | https://localhost:3000/api/Calculate/b | [BODY] | {  "array": [10,20,30],  "comment": "string"} |
| POST | https://localhost:3000/api/Calculate/c | [BODY] | {  "array": [10,20,30],  "comment": "string"} |
| GET | https://localhost:3000/api/history | [Uri] From, To, RequestId | https://localhost:3000/api/history?From=2022-11-24&To=2022-11-25&RequestId=01ce951f-db7e-46f4-80f6-5e5f4d7a78e4|


[Postgre Sql]: <https://www.postgresql.org/download/>
[EF Core]: <https://learn.microsoft.com/en-us/ef/core/get-started/overview/install>

