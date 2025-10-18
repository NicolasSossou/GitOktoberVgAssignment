namespace Infrastrucutre.Interfaces;

 public interface IFileService
{
    void Save<T>(string path, IEnumerable<T> products);
    IEnumerable<T> Load<T>(string path);
}
