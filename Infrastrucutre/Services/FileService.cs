using Infrastructure.Models;
using Infrastrucutre.Interfaces;
using System.Text.Json;
namespace Infrastructure.Services;

public class FileService : IFileService
{
    public void Save<T>(string path, IEnumerable<T> items)
    {
        var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }

    public IEnumerable<T> Load<T>(string path)
    {
        if (!File.Exists(path)) return Enumerable.Empty<T>();
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<IEnumerable<T>>(json) ?? Enumerable.Empty<T>();
    }
}
