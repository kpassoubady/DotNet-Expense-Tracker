using ExpenseTracker.Web.Data;
using ExpenseTracker.Web.DTOs;
using ExpenseTracker.Web.Middleware;
using ExpenseTracker.Web.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ExpenseTrackerContext>(options =>
{
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

   if (builder.Environment.IsDevelopment())
   {
      options.EnableSensitiveDataLogging();
      options.EnableDetailedErrors();
   }
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to DI container
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add API controllers
builder.Services.AddControllers();

builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
   app.UseHttpsRedirection();
}

app.UseRouting();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/health", () => Results.Ok(new HealthResponse("Healthy", DateTime.UtcNow)))
   .WithName("Health")
   .Produces<HealthResponse>(StatusCodes.Status200OK, "application/json");

app.Run();

record HealthResponse(string Status, DateTime TimestampUtc);
