using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ServerSideAPI;
using ServerSideAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IUpdateIdRepository, DbUpdateIdRepository>();
builder.Services.AddDbContext<IntraRaddningstjanstDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));
builder.Services.AddHostedService<PeriodicHostedService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
