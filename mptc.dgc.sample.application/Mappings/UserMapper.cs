using mptc.dgc.sample.application.DTOs.User;
using mptc.dgc.sample.application.Interfaces.IUser;
using mptc.dgc.sample.infrastructure.Models;

namespace mptc.dgc.sample.application.Mappings
{
    public class UserMapper : IUserMapper
    {
        public UserReadDto ToReadDto(User user)
        {
            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
            };
        }

        public User ToEntity(UserDto dto)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateEntity(User user, UserDto dto)
        {
            user.Name = dto.Name;
            user.Email = dto.Email;
        }
    }
}
