using Microsoft.EntityFrameworkCore;
using Quartz;
using WebApplication1;
using WebApplication1.Workers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opciones=>opciones.UseSqlServer(connectionString));
builder.Services.AddQuartz(q =>
    {
        var jobKey = new JobKey("SendEmailJob");
        q.AddJob<Worker>(opts => opts.WithIdentity(jobKey));

        q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("SendEmailJob-trigger")
            //This Cron interval can be described as "run every minute" (when second is zero)
            .WithCronSchedule("0 10 5 1/1 * ?")
        );
    });
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
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
