using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.UserDTOs;
using ExchangeBook.Services.Exceptions;
using ExchangeBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExchangeBook.models;

namespace ExchangeBook.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(IApplicationService applicationService, IConfiguration configuration,
        IMapper mapper)
            : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserReadOnlyDTO>> SignupUserAsync(UserSignUpDTO? userSignupDTO)
        {
            Console.WriteLine("role:" + userSignupDTO.UserRole.Value.ToString());
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, build a custom response
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                    });

                // instead of return BadRequest(new { Errors = errors });
                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }
            if (_applicationService == null)
            {
                throw new ServerGenericException("ApplicationServiceNull");
            }
            User? user = await _applicationService.UserService.GetUserByUsernameAsync(userSignupDTO!.Username!);
            if (user is not null)
            {
                throw new UserAlreadyExistsException("UserExists: " + user.Username);
            }
            User? returnedUser = await _applicationService.UserService.SignUpUserAsync(userSignupDTO);
            if (returnedUser is null)
            {
                throw new InvalidRegistrationException("InvalidRegistration");
            }
            var returnedUserDTO = _mapper.Map<UserReadOnlyDTO>(returnedUser);
            //return CreatedAtAction(nameof(GetUserById), new { id = returnedUserDTO.Id }, returnedUserDTO);
            return CreatedAtAction(nameof(GetUserById), new { id = returnedUserDTO.Id }, returnedUserDTO);
        }

        [HttpPost]
        public async Task<ActionResult<UserReadOnlyDTO>> SignUpStoreUserAsync(UserStoreSignupDTO userStoreSignupDTO)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, build a custom response
                var errors = ModelState
                    .Where(e => e.Value!.Errors.Any())
                    .Select(e => new
                    {
                        Field = e.Key,
                        Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                    });

                // instead of return BadRequest(new { Errors = errors });
                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }
            if (_applicationService == null)
            {
                throw new ServerGenericException("ApplicationServiceNull");
            }
            User? user = await _applicationService.UserService.GetUserByUsernameAsync(userStoreSignupDTO!.Username!);
            if (user is not null)
            {
                throw new UserAlreadyExistsException("UserExists: " + user.Username);
            }
            User? returnedUser = await _applicationService.UserService.SignUpStoreUserAsync(userStoreSignupDTO);
            if (returnedUser is null)
            {
                throw new InvalidRegistrationException("InvalidRegistration");
            }
            var returnedUserDTO = _mapper.Map<UserReadOnlyDTO>(returnedUser);
            //return CreatedAtAction(nameof(GetUserById), new { id = returnedUserDTO.Id }, returnedUserDTO);
            return CreatedAtAction(nameof(GetUserById), new { id = returnedUserDTO.Id }, returnedUserDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadOnlyDTO>> GetUserById(int id)
        {
            var user = await _applicationService.UserService.GetUserById(id);
            if (user is null)
            {
                throw new UserNotFoundException("UserNotFound");
            }

            var returnedUser = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedUser);
        }

        [HttpPost]
        public async Task<ActionResult<JwtTokenDTO>> LoginUserAsync(UserLoginDTO credentials)
        {
            var user = await _applicationService.UserService.VerifyAndGetUserAsync(credentials);
            if (user == null)
            {
                throw new UnauthorizedAccessException("BadCredentials");
            }
            var userToken = await _applicationService.UserService.CreateUserToken(user.Id, user.Username!, user.Email!,
                user.UserRole, _configuration["Authentication:SecretKey"]!);

            JwtTokenDTO token = new()
            {
                Token = userToken
            };

            return Ok(token);
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<UserPersonReadOnlyDTO>> GetUserPersonByUsername(string? username)
        {
            var returnedUserPersonDTO = await _applicationService.UserService.GetUserPersonByUsername(username);
            if (returnedUserPersonDTO is null)
            {
                throw new UserNotFoundException("UserNotFound");
            }

            return Ok(returnedUserPersonDTO);
        }
        //[HttpPut]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<UserDTO>> UpdateUserAccount(int id, UserDTO? userDTO)
        //{
        //    var userId = AppUser!.Id;
        //    if (id != userId)
        //    {
        //        throw new ForbiddenException("ForbiddenAccess");
        //    }
        //    var user = await _applicationService.UserService.UpdateUserAsync(userId, userDTO!);
        //    var returnedUserDTO = _mapper.Map<UserDTO>(user);
        //    return Ok(returnedUserDTO);
        //}

        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var userId = AppUser!.Id;
        //    if (id != userId)
        //    {
        //        throw new ForbiddenException("ForbiddenAccess");
        //    }

        //    await _applicationService.UserService.DeleteUserAsync(userId);
        //    return NoContent();
        //}
    }
}



