using ServerSideAPI;
using ServerSideAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<FetchSituations>();
//builder.Services.AddSingleton<FetchSituations>();
//builder.Services.AddHostedService(
//    provider => provider.GetRequiredService<FetchSituations>());
//builder.Services.AddHostedService<FetchSituations>();
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
