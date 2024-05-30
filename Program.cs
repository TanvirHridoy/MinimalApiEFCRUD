using MinimalApi.DIServices;
using MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AppApplicationServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline. 

// 100,000,00.00 dkk 100.000.00,00
#region Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
#endregion
var endpoints = new EndpointsConfigure();

endpoints.Configure(app);
await app.RunAsync();

