using mptc.dgc.sample.application.DTOs.User;

namespace mptc.dgc.sample.application.Interfaces.IUser
{
    public interface IUserMapper
    {
        UserReadDto ToReadDto(infrastructure.Models.User user);
        infrastructure.Models.User ToEntity(UserDto dto);
        void UpdateEntity(infrastructure.Models.User user, UserDto dto);
    }
}
