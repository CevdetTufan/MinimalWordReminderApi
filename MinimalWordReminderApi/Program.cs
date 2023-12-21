using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalWordReminderApi;
using MinimalWordReminderApi.Models;
using MinimalWordReminderApi.Repositories;
using MinimalWordReminderApi.Services;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connSting = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MinimalDbContext>(opt => opt.UseSqlServer(connSting));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWordRepository, WordRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWordService, WordService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1",
	new OpenApiInfo
	{
		Title = "Minimal Api Word Reminder",
		Description = "Minimal Api Word Reminder for showing Swagger",
		Version = "v1"
	});

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
						}
					},
					new string[] {}
				   }
				});

});

builder.Services.AddCors();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{

		ValidateIssuerSigningKey = true,
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		ClockSkew = TimeSpan.Zero,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
	};
});

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Merhaba Minimal API!").RequireAuthorization();


app.MapPost("/auth/login", async (IUserService db, [FromBody] UserLoginPostModel model) =>
{
	var user = await db.Login(model);
	if (user != null)
	{
		return Results.Ok(user);
	}
	else
	{
		return Results.Unauthorized();
	}
});

app.MapPost("/word/add", (IWordService service, [FromBody] WordPostModel model) =>
{
	try
	{
		return Results.Ok(service.Add(model));
	}
	catch
	{
		return Results.Problem("Hata Meydana Geldi", statusCode: 500);
	}
}).RequireAuthorization();


app.Run();