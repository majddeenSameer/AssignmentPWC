using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveAsync(string key, byte[] content);

        Task<byte[]> GetAsync(string key);

        Task DeleteAsync(string key);
    }
}
