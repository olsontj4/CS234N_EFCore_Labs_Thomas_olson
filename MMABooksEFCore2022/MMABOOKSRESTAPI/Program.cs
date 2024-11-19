using Microsoft.Extensions.Options;
using MMABooksEFClasses.MarisModels;

var builder = WebApplication.CreateBuilder(args);

// ADD CORS POLICY - IN A PRODUCTION APP LOCK THIS DOWN!
builder.Services.AddCors(Options => { Options.AddDefaultPolicy (
    builder => { builder.AllowAnyOrigin()
        .WithMethods("POST", "PUT", "DELETE", "GET", "OPTIONS")
        .AllowAnyHeader();
    });
});

// ADDING THE DBCONTEXT TO THE SERVICE
builder.Services.AddDbContext<MMABooksContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
