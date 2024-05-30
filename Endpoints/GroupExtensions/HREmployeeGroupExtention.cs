using Microsoft.AspNetCore.Mvc;
using MinimalApi.DTO;
using MinimalApi.Repository;

namespace MinimalApi.Endpoints.GroupExtensions;

public static class HREmployeeGroupExtention
{
    public static RouteGroupBuilder MapHREmployeeGroup(this RouteGroupBuilder group)
    {
        group.MapGet("/GetAllEmployees", async ([FromServices] EmployeeRepository repo) =>
        {
            var employees = await repo.GetAllEmployees();
            return Results.Ok(employees);
        }).WithName("GetAllEmployees");

        group.MapGet("/GetEmployeeById/{id}", async ([FromServices] EmployeeRepository repo, int id) =>
        {
            var employee = await repo.GetEmployeeById(id);
            return employee != null ? Results.Ok(employee) : Results.NotFound();
        }).WithName("GetEmployeeById");

        group.MapPost("/CreateEmployee", async ([FromServices] EmployeeRepository repo, Employee employee) =>
        {

            var newEmployee = await repo.AddEmployee(employee);
            return Results.CreatedAtRoute("GetEmployeeById", new { id = newEmployee.EmployeeId }, newEmployee);
        })
                    .WithName("CreateEmployee")
                    .WithOpenApi();

        group.MapPut("/UpdateEmployee/{id}", async ([FromServices] EmployeeRepository repo, int id, Employee employee) =>
        {
            if (id != employee.EmployeeId)
            {
                return Results.BadRequest();
            }

            var updatedEmployee = await repo.UpdateEmployee(employee);
            return updatedEmployee != null ? Results.Ok(updatedEmployee) : Results.NotFound();
        })
        .WithName("UpdateEmployee");

        group.MapDelete("/DeleteEmployee/{id}", async ([FromServices] EmployeeRepository repo, int id) =>
        {
            var employee = await repo.GetEmployeeById(id);
            if (employee == null)
            {
                return Results.NotFound();
            }

            await repo.DeleteEmployee(id);
            return Results.NoContent();
        })
        .WithName("DeleteEmployee");

        return group;
    }
}
