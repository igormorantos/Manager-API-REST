using Manager.Core.Structs;
using Manager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Services.Interfaces
{
    public interface IUserService
    {
        Task<Optional<UserDTO>> CreateAsync(UserDTO userDTO);
        Task<Optional<UserDTO>> UpdateAsync(UserDTO userDTO);
        Task RemoveAsync(Guid id);
        Task<Optional<UserDTO>> GetAsync(Guid id);
        Task<Optional<IList<UserDTO>>> GetAllAsync();
        Task<Optional<IList<UserDTO>>> SearchByNameAsync(string name);
        Task<Optional<IList<UserDTO>>> SearchByEmailAsync(string email);
        Task<Optional<UserDTO>> GetByEmailAsync(string email);
    }
}
