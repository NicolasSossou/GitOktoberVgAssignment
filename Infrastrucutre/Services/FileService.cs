using Infrastructure.Interfaces;
using System.Text.Json;
namespace Infrastructure.Services;

    public class FileService : IFileService
    {
        public void Save<T>(string filepath, IEnumerable<T> products)
        {
            try
            {
                string jsonText = JsonSerializer.Serialize(products, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(filepath, jsonText);
            }
            ()
            {
                Console.WriteLine("Could not save file");
            }
        }

        public IEnumerable<T> Load<T>(string filepath)
        {
            try
            {
                if (!File.Exists(filepath))
                    return Enumerable.Empty<T>();

                string jsonText = File.ReadAllText(filepath);

                IEnumerable<T>? products = JsonSerializer.Deserialize<IEnumerable<T>>(jsonText);

                if (products == null)
                    return Enumerable.Empty<T>();

                return products;
            }
             ()
            {
                Console.WriteLine("Could not load file");
                return Enumerable.Empty<T>();
            }
        }
    }

