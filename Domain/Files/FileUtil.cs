namespace Domain.Files;

using System;

public static class FileUtil
{

    public static readonly string ReadPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "INPUT.TXT"));
    public static readonly string WritePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OUTPUT.TXT"));
    
    
    public static string[] Read(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException($"Cannot found file: {inputFilePath}");
        }

        return File.ReadAllLines(inputFilePath);
    }
    
    public static void Write(string outputFilePath, object output)
    {
        try
        {
            File.WriteAllText(outputFilePath, output.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cannot write to file: {ex.Message}");
        }
    }
}