using ExchangeBook.Data;
using ExchangeBook.DTO.UserDTOs;
using ExchangeBook.models;

namespace ExchangeBook.Services
{
    public interface IUserService
    {

        Task<User?> SignUpUserAsync(UserSignUpDTO request);
        Task<User?> SignUpStoreUserAsync(UserStoreSignupDTO request);
        Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials);
        Task<User?> UpdateUserAsync(int userId, UserDTO userDTO);
        Task<User?> GetUserByUsernameAsync(string username);
        //Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize,
        //    UserFiltersDTO userFiltersDTO);
        Task<UserPersonReadOnlyDTO?> GetUserPersonByUsername(string? username);


        Task<string> CreateUserToken(int userId, string? userName, string? email, UserRole? userRole, int? roleEntityId,
            string appSecurityKey);

        //Task DeleteUserAsync(int id);




    }
}

