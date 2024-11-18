
using Lab4;
using McMaster.Extensions.CommandLineUtils;

[Command(Name = "Lab4", Description = "Lab4")]
[Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
internal class Program
{
    static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    private void OnExecute()
    {
        Console.WriteLine("dotnet run Lab4 help");
        Console.WriteLine("run [-i|--input] [-o|--output] {lab1/lab2/lab3} - run application");
        Console.WriteLine("version - version of project");
        Console.WriteLine("set-path -p|--path {path} - set default files path");
    }

    private void OnUnknownCommand(CommandLineApplication app)
    {
        Console.WriteLine("dotnet run Lab4 help");
        Console.WriteLine("run [-I|--input] [-o|--output] {lab1/lab2/lab3} - run application");
        Console.WriteLine("version - version of project");
        Console.WriteLine("set-path -p|--path {path} - set default files path");
    }
    
}

[Command(Name = "version", Description = "version")]
class VersionCommand
{
    private void OnExecute()
    {
        Console.WriteLine("Author: Kovtyn Olena 32");
        Console.WriteLine("Version: 1.0.0");
    }
}

[Command(Name = "run", Description = "Run a specific lab")]
class RunCommand
{
    
    [Argument(0, "lab", "Specify lab to run (lab1/lab2/lab3)")]
    public string? Lab { get; set; }

    [Option("-I|--input", "Input file", CommandOptionType.SingleValue)]
    public string? InputFile { get; set; }

    [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
    public string? OutputFile { get; set; }


    private void OnExecute()
    {
        var homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var env = Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.User);
        var inputPath = InputFile ?? (env == null ? Path.Combine(homePath, "INPUT.TXT") : Path.Combine(env, "INPUT.TXT"));
        var outputPath = OutputFile ?? (env == null ? Path.Combine(homePath, "OUTPUT.TXT") : Path.Combine(env, "OUTPUT.TXT"));

        if (!File.Exists(inputPath))
        {
            throw new Exception($"Cannot find input file {inputPath}");
        }

        var selector = new LabSelector(Lab, inputPath, outputPath);
        selector.SelectAndExecute();
    }
}

[Command(Name = "set-path", Description = "Set input/output path")]
class SetPathCommand
{
    [Option("-p|--path", "Setting path for input/output files", CommandOptionType.SingleValue)]
    public required string Path { get; set; }

    private void OnExecute()
    {
        Environment.SetEnvironmentVariable("LAB_PATH", Path, EnvironmentVariableTarget.User);
        Console.WriteLine($"Selected Default Files Path: {Path}");
    }
}