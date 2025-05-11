using CSharp.Service.Contracts;
using CSharp.Service.Models;
using CSharp.Service.Repositories.Interfaces;
using CSharp.Service.Services.Interfaces;

namespace CSharp.Service.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateAsync(UserRequest request)
        {
            await _userRepository.AddAsync(new User()
            {
                Lastname = request.Lastname,
                Firstname = request.Firstname,
                Age = request.Age
            });
        }
    }
}
