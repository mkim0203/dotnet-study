using RestapiTest.Models;
using RestapiTest.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// appsettings에서 데이터로드 후 DI 등록
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

// 바로 api추가후 사용
app.MapGet("/minimal-test", (SampleData testData) =>
{
    return Results.Ok(testData);
});
app.MapGet("/minimal-test/{id}", (SampleData testData, string id) =>
{
    return Results.Ok(new { Data = testData, Id = id, Time = DateTimeOffset.UtcNow });
});
app.MapPost("/minimal-test", (SampleData testData) =>
{
    return Results.Ok(new { Data = testData, Time = DateTimeOffset.UtcNow });
});
app.MapDelete("/minimal-test/{id}", (string id) => { TypedResults.NotFound("삭제 실패"); });
app.MapPut("/minimal-test/{id}", (string id) => { TypedResults.BadRequest("수정 실패"); });


// map group 이 있을경우.
app.MapGroup("users").MapUsersApi();

app.Run();
