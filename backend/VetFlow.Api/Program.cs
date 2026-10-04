using VetFlow.Api.Middleware;
using VetFlow.Application;
using VetFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ─── Core Services ───────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "VetFlow API",
        Version = "v1",
        Description = "Multi-tenant veterinary practice management API"
    });
});

// ─── Error Handling & Conventions ───────────────────────────────────────────
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// ─── Application Layers ──────────────────────────────────────────────────────
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// ─── HTTP Pipeline ───────────────────────────────────────────────────────────
var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
