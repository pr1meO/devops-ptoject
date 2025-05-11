using CSharp.Service.Contracts;

namespace CSharp.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserRequest request);
    }
}
