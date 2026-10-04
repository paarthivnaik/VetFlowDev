using VetFlow.Application;
using VetFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ─── Services ────────────────────────────────────────────────────────────────
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

// Register layer services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// ─── Application Pipeline ───────────────────────────────────────────────────
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Marker class for WebApplicationFactory in integration tests
public partial class Program { }
