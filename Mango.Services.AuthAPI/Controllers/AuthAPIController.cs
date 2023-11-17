﻿using Mango.MessageBus;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        protected ResponseDto _response;

        public AuthAPIController(
            IAuthService authService,
            IMessageBus messageBus,
            IConfiguration configuration)
        {
            _authService = authService;
            _response = new();
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            await 
                _messageBus.PublishMessage(model.Email, _configuration.GetValue<string>("RegisterUserQueue:registeruser"));

            return Ok(_response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            /*
             * {
             *  "email": "administrador1@administrador1.com",
             *  "name": "administrador",
             *  "phoneNumber": "123456789",
             *  "password": "Admini$trador1"
             * },
             * * {
             *  "email": "customer1@customer1.com",
             *  "name": "customer1",
             *  "phoneNumber": "123456789",
             *  "password": "Cu$tomer1"
             * }
             */
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User is null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or Password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }
        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessfull = await _authService.AssignRole(model.Email, model.Role.ToUpper());

            if (!assignRoleSuccessfull)
        {
                _response.IsSuccess=false;
                _response.Message = "Error Encountered";
                return BadRequest(_response);

            }
            return Ok(_response);
        }

    }
}
