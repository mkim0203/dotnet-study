using Microsoft.AspNetCore.Http.HttpResults;
using RestapiTest.Models;

namespace RestapiTest.Routes;

public static class UsersApi
{
    public static RouteGroupBuilder MapUsersApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", SearchAllUsers);
        return group;
    }

    private static async ValueTask<Results<Ok<SampleData>, NotFound>> SearchAllUsers(SampleData sample)
    {
        await Task.Delay(100);
        return TypedResults.Ok(sample);
    }
}
