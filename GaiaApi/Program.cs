using GaiaApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ????? DbContext
builder.Services.AddDbContext<GaiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
