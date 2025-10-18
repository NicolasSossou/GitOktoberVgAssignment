using Infrastructure.Models;
using Infrastrucutre.Interfaces;
using System.Text.Json;
namespace Infrastructure.Services;

    public class FileService : IFileService
    {
        public void Save<T>(string path, IEnumerable<T> items)
        {
            try
            {
                string jsonText = JsonSerializer.Serialize(items, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(path, jsonText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not save file: {ex.Message}");
            }
        }

        public IEnumerable<T> Load<T>(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return Enumerable.Empty<T>();

                string jsonText = File.ReadAllText(path);

                IEnumerable<T>? items = JsonSerializer.Deserialize<IEnumerable<T>>(jsonText);

                if (items == null)
                    return Enumerable.Empty<T>();

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not load file: {ex.Message}");
                return Enumerable.Empty<T>();
            }
        }
    }

