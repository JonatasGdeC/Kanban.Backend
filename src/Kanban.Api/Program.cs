using Kanban.Api.Token;
using Kanban.Application;
using Kanban.Domain.Security.Tokens;
using Kanban.Infrastructure;
using Microsoft.OpenApi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(configurationManager: builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddSwaggerGen(setupAction: config =>
{
    config.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = """
                      JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token.
                      Example: 'Bearer 12345abcdef'
                      """,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });

    config.AddSecurityRequirement(securityRequirement: document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference(referenceId: "Bearer", hostDocument: document),
            []
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();

app.Run();