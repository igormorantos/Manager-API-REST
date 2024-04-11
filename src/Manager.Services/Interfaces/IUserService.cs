using Manager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateAsync(UserDTO obj);
        Task<UserDTO> UpdateAsync(UserDTO obj);
        Task RemoveAsync(Guid id);
        Task<UserDTO> Get(Guid id);
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> GetByEmailAsync(string email);
        Task<List<UserDTO>> SearchByEmailAsync(string email);
        Task<List<UserDTO>> SearchByNameAsync(string name);
    }
}
