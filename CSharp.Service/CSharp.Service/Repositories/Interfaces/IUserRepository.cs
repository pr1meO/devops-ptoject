using CSharp.Service.Models;

namespace CSharp.Service.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}
