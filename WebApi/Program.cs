using Application.Features.TaskItems.Command.Create;
using Application.Mappings.TaskItemProfile;
using Application.Repository;
using Application.Validators;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fvalidation => { fvalidation.RegisterValidatorsFromAssembly(typeof(CreateTaskItemCommandValidator).Assembly); }); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(TaskItemProfile).Assembly);
});

builder.Services.AddAutoMapper(typeof(Application.AssemblyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => 
{
    var xmlFile= $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

//Frontend ile Backend arasýnda iletiþimi kurmak için Cors Ýþlemi yapýyoruz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular portumuz
              .AllowAnyHeader()
              .AllowAnyMethod();                    //Tüm methodlara izin verir
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseCors("AllowAngular");

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
