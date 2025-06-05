using mptc.dgc.sample.application.DTOs;
using mptc.dgc.sample.application.DTOs.Success;
using mptc.dgc.sample.application.DTOs.User;

namespace mptc.dgc.sample.application.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task<ResponsePagingDto<UserReadDto>> GetUsersPagedAsync(PaginationQueryParams param);
        public Task<UserReadDto?> GetUserByIdAsync(int userId);
        public Task<UserReadDto> UpdateUserAsync(int userId, UserDto user);
        public Task<UserReadDto> CreateUserAsync(UserDto user);
        public Task DeleteUserAsync(int userId);
    }
}
