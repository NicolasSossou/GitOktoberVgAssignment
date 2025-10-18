using Infrastructure.Models;
using Infrastrucutre.Interfaces;
namespace Infrastructure.Services;

public class FileService
{
    private readonly string _filePath;

    public FileService(string filePath = "products.json") 
    {
        _filePath = filePath;
    }

    public bool SaveJsonContentToFile(string jsonContent)
    {
        try
        {
            File.WriteAllText(_filePath, jsonContent);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GetJsonContentFromFile()
    {
        try
        {
            return File.Exists(_filePath)
                ? File.ReadAllText(_filePath)
                : string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}   
