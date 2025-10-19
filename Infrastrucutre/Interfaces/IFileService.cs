namespace Infrastructure.Interfaces;

using System.Collections.Generic;
    public interface IFileService
    {
        void Save<T>(string filepath, IEnumerable<T> products);

        IEnumerable<T> Load<T>(string filepath);
    }

