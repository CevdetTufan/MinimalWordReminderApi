using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinimalWordReminderApi;

var builder = WebApplication.CreateBuilder(args);

var connSting = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MinimalDbContext>(opt => opt.UseSqlServer(connSting));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/", () => "Merhaba Minimal API!");

app.MapGet("/word/get{id}", async (MinimalDbContext db, int id) => await db.Words.FindAsync(id));

app.MapGet("/word/list", async (MinimalDbContext db) => await db.Words.ToListAsync());

app.Run();