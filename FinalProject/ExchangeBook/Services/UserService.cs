using AutoMapper;
using ExchangeBook.Data;
using ExchangeBook.DTO.UserDTOs;
using ExchangeBook.models;
using ExchangeBook.Repositories;
using ExchangeBook.Security;
using ExchangeBook.Services.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExchangeBook.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly ILogger<UserService>? _logger;
        private readonly IMapper? _mapper;

        public UserService(IUnitOfWork? unitOfWork, ILogger<UserService>? logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        //
        public async Task<string> CreateUserToken(int userId, string? username, string? email, UserRole? userRole,string? appSecurityKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSecurityKey!));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            int? roleEntityId = 0;
            string? phoneNumber = "";
            if(userRole.Value == UserRole.PERSONAL)
            {
                UserPersonReadOnlyDTO? userPerson = await GetUserPersonByUsername(username);
                roleEntityId = userPerson.personId;
            }
            if (userRole.Value == UserRole.STORE)
            {
                UserStoreReadOnlyDTO? userStore = await GetUserStoreByUsername(username);
                roleEntityId = userStore.storeId;

            }
            var claimsInfo = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username!),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email,email!),
                new Claim(ClaimTypes.Role, userRole.ToString()!),

                new Claim("name", username!),
                new Claim("id", userId.ToString()),
                new Claim("email", email!),
                new Claim("role", userRole.Value.ToString()!),
                new Claim("roleEntityId", roleEntityId.ToString())
                //new Claim("roleEntityId", userPerson.personId.ToString())


            };
            if (roleEntityId.HasValue)
            {
                claimsInfo.Add(new Claim("RoleEntityId", roleEntityId.Value.ToString()));
            }

            var jwtSecurityToken = new JwtSecurityToken(null, null, claimsInfo, DateTime.UtcNow,
                DateTime.UtcNow.AddHours(3), signingCredentials);

            // Serialize the token
            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            //return "Bearer " + userToken;
            return userToken;
        }

        //public async Task DeleteUserAsync(int id)
        //{
        //    bool deleted;
        //    try
        //    {
        //        deleted = await _unitOfWork!.UserRepositorty.DeleteAsync(id);
        //        if (!deleted)
        //        {
        //            throw new UserNotFoundException("UserNotFound");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
        //        throw;
        //    }
        //}

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            User? user;
            try
            {
                user = await _unitOfWork!.UserRepositorty.GetByUsernameAsync(username);
                _logger!.LogInformation("{Message}", "User: " + user + " found and returned");
            }
            catch (UserNotFoundException e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

        public async Task<User?> SignUpUserAsync(UserSignUpDTO signUpDTO)
        {
            User? user;
            Person? person;
            //Store store;
            try
            {
                user = ExtractUser(signUpDTO);
                User? existingUser = await _unitOfWork!.UserRepositorty.GetByUsernameAsync(user.Username);
                if (existingUser != null)
                {
                    throw new UserAlreadyExistsException("UserExists: " + existingUser.Username);
                }
                user.Password = BCryptUtil.Encrypt(user.Password!);
                await _unitOfWork!.UserRepositorty.AddAsync(user);

                if (user!.UserRole.HasValue && user.UserRole.Value.ToString().Equals("PERSONAL"))
                {
                    person = ExtractPerson(signUpDTO);
                    if (await _unitOfWork!.PersonRepository.GetByPhoneNumber(person.PhoneNumber) is not null)
                    {
                        throw new PersonAlreadyExistsException("StudentExists");
                    }
                    await _unitOfWork.PersonRepository.AddAsync(person);
                    user.Person = person;
                }
                else
                {
                    throw new InvalidRoleException("invalid Role");
                }
                await _unitOfWork.SaveAsync();
                return user;
            }catch(Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }

        public async Task<User?> SignUpStoreUserAsync(UserStoreSignupDTO signUpDTO)
        {
            User? user;
            Store? store;
            //Store store;
            try
            {
                user = ExtractUser(signUpDTO);
                User? existingUser = await _unitOfWork!.UserRepositorty.GetByUsernameAsync(user.Username);
                if (existingUser != null)
                {
                    throw new UserAlreadyExistsException("UserExists: " + existingUser.Username);
                }
                user.Password = BCryptUtil.Encrypt(user.Password!);
                await _unitOfWork!.UserRepositorty.AddAsync(user);

                if (user!.UserRole.HasValue && user.UserRole.Value.ToString().Equals("STORE"))
                {
                    store = ExtractStore(signUpDTO);
                    //if (await _unitOfWork!.PersonRepository.GetByAddress(store.Address) is not null)
                    //{
                    //    throw new PersonAlreadyExistsException("StudentExists");
                    //}
                    await _unitOfWork.StoreRepository.AddAsync(store);
                    user.Store = store;
                }
                else
                {
                    throw new InvalidRoleException("invalid Role");
                }
                await _unitOfWork.SaveAsync();
                return user;
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }
        public async Task<User?> UpdateUserAsync(int userId, UserDTO userDTO)
        {
            User? existingUser;
            User? user;
            try
            {
                existingUser = await _unitOfWork!.UserRepositorty.GetAsync(userId);
                if (existingUser == null) return null;

                var userToUpdate = _mapper!.Map<User>(userDTO);

                user = await _unitOfWork.UserRepositorty.UpdateUserAsync(userId, userToUpdate);
                await _unitOfWork.SaveAsync();
                _logger!.LogInformation("{Message}", "User: " + user + " updated successfully");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }
        public async Task<User?> GetUserById(int id)
        {
            User? user;
            try
            {
                user = await _unitOfWork!.UserRepositorty.GetAsync(id);
                _logger!.LogInformation("{Message}", "User with id: " + id + " Success");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }


        public async Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials)
        {
            User? user = null;

            try
            {
                user = await _unitOfWork!.UserRepositorty.GetUserAsync(credentials.Username!, credentials.Password!);
                _logger!.LogInformation("{Message}", "User: " + user + " found and returned");
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
            return user;
        }

       
        private Store ExtractStore(UserStoreSignupDTO? signupDTO)
        {
            return new Store()
            {
                Name = signupDTO!.Name!,
                Address = signupDTO!.Address,
            };
        }

        public async Task<UserPersonReadOnlyDTO?> GetUserPersonByUsername(string? username)
        {
            UserPersonReadOnlyDTO? dto;
            try
            {
                dto = await _unitOfWork!.UserRepositorty.GetUserPersonInfoAsync(username!);
                _logger!.LogInformation("{Message}", "User with username: " + username + " Success");
                return dto;
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }
        }
        public async Task<UserStoreReadOnlyDTO> GetUserStoreByUsername(string? username)
        {
            UserStoreReadOnlyDTO? dto;
            try
            {
                dto = await _unitOfWork!.UserRepositorty.GetUserStoreInfoAsync(username!);
                _logger!.LogInformation("{Message}", "User with username: " + username + " Success");
                return dto;
            }
            catch (Exception e)
            {
                _logger!.LogError("{Message}{Exception}", e.Message, e.StackTrace);
                throw;
            }

        }

        private User ExtractUser(UserSignUpDTO signupDTO)
        {
            return new User()
            {
                Username = signupDTO.Username,
                Password = signupDTO.Password,
                Email = signupDTO.Email,
                UserRole = signupDTO.UserRole
            };
        }
        private User ExtractUser(UserStoreSignupDTO signupDTO)
        {
            return new User()
            {
                Username = signupDTO.Username,
                Password = signupDTO.Password,
                Email = signupDTO.Email,
                UserRole = signupDTO.UserRole
            };
        }

        private Person ExtractPerson(UserSignUpDTO? signupDTO)
        {
            return new Person()
            {
                PhoneNumber = signupDTO!.PhoneNumber!,
                Firstname = signupDTO!.Firstname,
                Lastname = signupDTO!.Lastname,

            };
        }
    }
}
