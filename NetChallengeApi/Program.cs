using NetChallenge.Database;
using Microsoft.EntityFrameworkCore;
using NetChallenge.Database.Interfaces;
using Microsoft.AspNetCore.Hosting;
using NetChallenge.WebApi.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DotNetChallengeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DotNetChallengeConnection"))
);
builder.Services.AddScoped<IApiRepository, ApiRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DotNetChallengeContext>();
    context.Database.Migrate();
}

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
