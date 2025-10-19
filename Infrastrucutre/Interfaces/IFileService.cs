namespace Infrastructure.Interfaces;

using System.Collections.Generic;

public interface IFileService
{
    bool SaveJsonContentToFile(string jsonContent);
    string GetJsonContentFromFile();
}

