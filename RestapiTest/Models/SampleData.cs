namespace RestapiTest.Models;

public sealed record SampleData
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public int Number { get; set; }
    public bool IsUsed { get; set; }
}
