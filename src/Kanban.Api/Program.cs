using Kanban.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();