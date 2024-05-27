using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.DTO;
using MinimalApi.Repository;

var builder = WebApplication.CreateBuilder(args);


#region DI Container
builder.Services.AddDbContext<EmployeeDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDb")));
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
#region Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
#endregion

#region Endpoints
app.MapGet("/api/employees", async ([FromServices] EmployeeRepository repo) =>
{
    var employees = await repo.GetAllEmployees();
    return Results.Ok(employees);
}).WithName("GetAllEmployees").WithOpenApi();

app.MapGet("/api/employees/{id}", async ([FromServices] EmployeeRepository repo, int id) =>
{
    var employee = await repo.GetEmployeeById(id);
    return employee != null ? Results.Ok(employee) : Results.NotFound();
}).WithName("GetEmployeeById").WithOpenApi();

app.MapPost("/api/employees", async ([FromServices] EmployeeRepository repo, Employee employee) =>
{

    var newEmployee = await repo.AddEmployee(employee);
    return Results.CreatedAtRoute("GetEmployeeById", new { id = newEmployee.EmployeeId }, newEmployee);
})
            .WithName("CreateEmployee")
            .WithOpenApi();

app.MapPut("/api/employees/{id}", async ([FromServices] EmployeeRepository repo, int id, Employee employee) =>
{
    if (id != employee.EmployeeId)
    {
        return Results.BadRequest();
    }

    var updatedEmployee = await repo.UpdateEmployee(employee);
    return updatedEmployee != null ? Results.Ok(updatedEmployee) : Results.NotFound();
})
.WithName("UpdateEmployee")
.WithOpenApi();

app.MapDelete("/api/employees/{id}", async ([FromServices] EmployeeRepository repo, int id) =>
{
    var employee = await repo.GetEmployeeById(id);
    if (employee == null)
    {
        return Results.NotFound();
    }

    await repo.DeleteEmployee(id);
    return Results.NoContent();
})
.WithName("DeleteEmployee")
.WithOpenApi();
#endregion


app.Run();

