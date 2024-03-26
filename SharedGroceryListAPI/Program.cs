using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SharedGroceryListAPI.Context;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;


builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:3000", "http://localhost:5173", "https://appname.azurestaticapps.net");
        });
});

var configuration = builder.Configuration;

//SQL connection test
using (MySqlConnection connection = new MySqlConnection(builder.Configuration.GetConnectionString("Database")))
{
    Console.WriteLine("Test: " + builder.Configuration.GetConnectionString("Database").Length);
    try
    {
        connection.Open();
        Console.WriteLine("Database connected!");
    }
    catch 
    {
               
        Console.WriteLine("[!] Database connection error.");
    }

    connection.Close();
}


builder.Services.AddControllers();
builder.Services.AddDbContext<DBContext_SGL>(options => options.UseMySql(configuration.GetConnectionString("Database"), ServerVersion.AutoDetect(configuration.GetConnectionString("Database"))));

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

app.UseCors("CORSPolicy");

app.UseAuthentication();
app.UseRouting(); 
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();