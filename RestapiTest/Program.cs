using RestapiTest.Models;
using RestapiTest.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI �� setting ���� �ε�
SampleData sampleData = builder.Configuration.GetSection("SampleData").Get<SampleData>() ?? throw new InvalidOperationException("SampleData configuration");
builder.Services.AddSingleton<SampleData>(sampleData);

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

app.MapGet("/minimal-test", (SampleData testData) =>
{
    return Results.Ok(testData);
});

// map group �� �������.
app.MapGroup("users").MapUsersApi();

app.Run();