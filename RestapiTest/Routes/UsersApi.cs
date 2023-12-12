using Microsoft.AspNetCore.Http.HttpResults;
using RestapiTest.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Text.Json;

namespace RestapiTest.Routes;

public static class UsersApi
{
    public static RouteGroupBuilder MapUsersApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", SearchAllUsers);
        group.MapGet("/file", DownloadAllUsers);
        group.MapPost("/", AddUser);
        group.MapDelete("/{id}", DeleteUser);
        group.MapPut("/{id}", UpdateUser);
        return group;
    }

    private static async ValueTask<Results<Ok, BadRequest<string>>> UpdateUser(string id, SampleData sample)
    {
        await Task.Delay(100);
        return TypedResults.BadRequest($"{id} update fail");
    }

    private static async ValueTask<Results<Ok, NotFound<string>>> DeleteUser(string id)
    {
        await Task.Delay(100);
        return TypedResults.NotFound($"{id} not found");
    }

    private static async ValueTask<Results<Ok<SampleData>, NotFound>> AddUser(SampleData sample)
    {
        await Task.Delay(100);
        return TypedResults.Ok(sample);
    }



    private static async ValueTask<Results<Ok<SampleData>, NotFound>> SearchAllUsers(SampleData sample)
    {
        await Task.Delay(100);
        return TypedResults.Ok(sample);
    }

    private static async ValueTask<FileStreamHttpResult> DownloadAllUsers(SampleData sample)
    {

        byte[] byteArray = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sample, new JsonSerializerOptions() { WriteIndented = true }));
        MemoryStream stream1 = new MemoryStream(byteArray);
        return TypedResults.File(stream1, Application.Octet);
    }
}
