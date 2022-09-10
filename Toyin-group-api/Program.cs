
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Toyin_group_api.Core.Data;
using Toyin_group_api.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InitServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ServicesBootstrapper).Assembly);
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseInMemoryDatabase("toyinDb");
});
builder.Services.AddDbContextFactory<AppDbContext>(x =>
{
    x.UseInMemoryDatabase("toyinDb");
}, ServiceLifetime.Scoped);


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
//app.UseCors();

app.Run();






