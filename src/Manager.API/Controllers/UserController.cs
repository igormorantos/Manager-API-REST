using AutoMapper;
using Manager.API.Utils;
using Manager.API.ViewModels;
using Manager.API.ViewModes;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Manager.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {

                var userDTO = _mapper.Map<UserDTO>(userViewModel);
                var userCreated = await _userService.CreateAsync(userDTO);

                // if (HasNotifications())
                // return Result();

                return Ok(new ResultViewModel
                {
                    Message = "Usuário criado com sucesso!",
                    Success = true,
                    Data = userCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/users/update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);
                var userUpdated = await _userService.UpdateAsync(userDTO);

                //if (HasNotifications())
                //return Result();

                return Ok(new ResultViewModel
                {
                    Message = "Usuário atualizado com sucesso!",
                    Success = true,
                    Data = userUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/v1/users/remove/{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            try
            {
                await _userService.RemoveAsync(id);

                //if (HasNotifications())
                //  return Result();

                return Ok(new ResultViewModel
                {
                    Message = "Usuário removido com sucesso!",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var user = await _userService.Get(id);

                // if (HasNotifications())
                //   return Result();

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o ID informado.",
                        Success = true,
                        Data = user
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }
        }


        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allUsers = await _userService.GetAll();

                //if (HasNotifications())
                //  return Result();

                return Ok(new ResultViewModel
                {
                    Message = "Usuários encontrados com sucesso!",
                    Success = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }
        }


        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-by-email")]
        public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
        {
            try
            {
                var user = await _userService.GetByEmailAsync(email);

                //if (HasNotifications())
                // return Result();

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o email informado.",
                        Success = true,
                        Data = user
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-name")]
        public async Task<IActionResult> SearchByNameAsync([FromQuery] string name)
        {
            try
            {
                
                var allUsers = await _userService.SearchByNameAsync(name);

                if (allUsers == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o nome informado",
                        Success = false,
                        Data = null
                    });
                }
                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Success = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }

        }


        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-email")]
        public async Task<IActionResult> SearchByEmailAsync([FromQuery] string email)
        {
            try
            {
                var allUsers = await _userService.SearchByEmailAsync(email);

                //if (HasNotifications())
                //  return Result();

                if (allUsers == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o email informado",
                        Success = true,
                        Data = null
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Success = true,
                    Data = allUsers          

                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(ex));
            }
        }


    }
}
