using Medallion.Shell;

namespace DockerTest.Utils;

internal class RunCommand
{
    public static async Task<CommandResult> RunAsync(string command, CancellationToken cancellationToken = default)
    {
        using Command commandExecutor = Environment.OSVersion.Platform == PlatformID.Win32NT
            ? Command.Run("cmd.exe", new[] { "/c", command }, options => options.CancellationToken(cancellationToken))
            : Command.Run("/bin/sh", new[] { "-c", command }, options => options.CancellationToken(cancellationToken));
        return await commandExecutor.Task.ConfigureAwait(false);
    }
}
