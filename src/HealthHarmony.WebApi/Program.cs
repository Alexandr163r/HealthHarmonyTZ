using HealthHarmony;
using HealthHarmony.BL.Services;
using HealthHarmony.DAL;
using HealthHarmony.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<IRecordServiceValidator, RecordServiceValidator>();

string appDir = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName) + "\\HealthHarmony.DAL\\DB\\";

builder.Services.AddDbContext<SQLiteDBContext>(options =>
    options.UseSqlite($"Data Source ={appDir}HealthHarmony.db"));

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