using AutoMapper;
using EscNet.Cryptography.Algorithms;
using EscNet.Cryptography.Interfaces;
using Manager.Core.Exceptions;
using Manager.Domain.Entity;
using Manager.Infrastructure.Interfaces;
using Manager.Infrastructure.Repositories;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRijndaelCryptography _rijndaelCryptography;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository, IRijndaelCryptography rijndaelCryptography)
        {
            _mapper = mapper;
            _rijndaelCryptography = rijndaelCryptography;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);

            if (userExists != null)
            {
                throw new DomainException("Já existe um usuario cadastrado com esse email");
            }

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDTO)
        {
            var userExists = await _userRepository.Get(userDTO.Id);

            if (userExists == null)
            {
                throw new DomainException("Não existe o usuario cadastrado com o dado informado");
            }

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userCreated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task RemoveAsync(Guid id)
        {
           await _userRepository.Remove(id);
        }

        public async Task<UserDTO> Get(Guid id)
        {
            var user = await _userRepository.Get(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var allUsers = await _userRepository.GetAll();

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> SearchByEmailAsync(string email)
        {
            var allUsers = await _userRepository.SearchByEmail(email);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByNameAsync(string name)
        {
            Expression<Func<User, bool>> filter = u => u.Name.ToLower().Contains(name.ToLower());

            var allUsers = await _userRepository.SearchByName(name);
            var allUsersDTO = _mapper.Map<IList<UserDTO>>(allUsers);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }
    }
}