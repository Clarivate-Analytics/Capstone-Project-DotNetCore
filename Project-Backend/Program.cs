using Microsoft.EntityFrameworkCore;
using Project_Backend.Db_Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Inject DbContext
//builder.Services.AddDbContext<TrackerDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("TrackerDbConnectionString")));


builder.Services.AddDbContext<RegistrationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("RegistrationDbConnectionString")));

builder.Services.AddDbContext<OrdersDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDbConnectionString")));

builder.Services.AddDbContext<VendorDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("VendorDbConnectionString")));





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
