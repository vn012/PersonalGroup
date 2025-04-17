using Aplication.Interfaces;
using Infra.IoC;
using PersonalGroupAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddSignalR(); // Adiciona SignalR
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder => builder
            .WithOrigins("http://localhost:3000") // ou a origem do seu frontend
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // isso permite withCredentials funcionar
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INotificationHub, NotificationHubAdapter>();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NoteHub>("/noteHub"); // Mapeia o hub SignalR

app.Run();