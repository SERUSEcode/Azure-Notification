using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ServerSideAPI;
using ServerSideAPI.Model;
using ServerSideAPI.Model.Message;
using ServerSideAPI.Model.SituationTb;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Facebook;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IUpdateIdRepository, DbUpdateIdRepository>();

builder.Services.AddDbContext<IntraRaddningstjanstDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));
builder.Services.AddScoped<ISituationTbRepository, DbSituationTbRepository>();
builder.Services.AddScoped<IMessageRepository, DbMessageRepository>();
//builder.Services.AddHostedService<PeriodicHostedService>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddFacebook(facebookOptions =>
{
	//facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
	//facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
	IConfigurationSection FBAuthNSection =
	   builder.Configuration.GetSection("Authentication:FB");
	facebookOptions.AppId = "5993682193976683";
    facebookOptions.AppSecret = "ed37dd48d17ebaddb4e29c7c1d87ee77";
    facebookOptions.SaveTokens = true;
});




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
