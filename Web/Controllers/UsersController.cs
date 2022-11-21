using Application.Common.Interfaces;
using Application.DTOS;
using Application.Request;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        [HttpGet]
        [Route("Authenticate")]
        public async Task<Application.DTOS.Response<AuthDTO>> Authenticate(string username, string password)
        {
            Application.DTOS.Response<AuthDTO> response = new Application.DTOS.Response<AuthDTO>();
            AuthRequest authRequest = new AuthRequest();
            authRequest.Username = username;
            authRequest.Password = password;

            try
            {
                response = await _usersRepository.Authenticate(authRequest);

            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Application.DTOS.Response<string>> Register(AuthRequest request)
        {
            Application.DTOS.Response<string> response = new Application.DTOS.Response<string>();
            try
            {
                return response = await _usersRepository.Register(request);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<Application.DTOS.Response<List<UsersDTO>>> GetAllUsers()
        {
            try
            {
                return await _usersRepository.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
