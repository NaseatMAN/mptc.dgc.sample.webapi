using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using mptc.dgc.sample.application.Constants;
using mptc.dgc.sample.application.Constants.User;
using mptc.dgc.sample.application.DTOs;
using mptc.dgc.sample.application.DTOs.Success;
using mptc.dgc.sample.application.DTOs.User;
using mptc.dgc.sample.application.Exceptions;
using mptc.dgc.sample.application.Helpers;
using mptc.dgc.sample.application.Interfaces.IUser;
using mptc.dgc.sample.infrastructure.Models;

namespace mptc.dgc.sample.application.Repositories;

public class UserRepository(SampleContext dbContext, IUserMapper mapper, IHttpContextAccessor contextAccessor)
    : IUserRepository
{
    public async Task<ResponsePagingDto<UserReadDto>> GetUsersPagedAsync(PaginationQueryParams param)
    {
        return await dbContext.Users
            .AsNoTracking()
            .OrderBy(u => u.Id)
            .ToPagedResultAsync(param.Skip, param.Top, mapper.ToReadDto, contextAccessor.HttpContext);
    }

    public async Task<UserReadDto?> GetUserByIdAsync(int userId)
    {
        var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new NotFoundException(string.Format(ErrorMessageConstants.ResourceNotFoundById, "User", userId),
                ErrorCodeConstants.NotFound);
        var result = mapper.ToReadDto(user);
        return result;
    }

    public async Task<UserReadDto> UpdateUserAsync(int userId, UserDto user)
    {
        var existUser = await dbContext.Users.FindAsync(userId);
        if (existUser == null)
            throw new NotFoundException(string.Format(ErrorMessageConstants.ResourceNotFoundById, "User", userId),
                ErrorCodeConstants.NotFound);

        mapper.UpdateEntity(existUser, user);

        dbContext.Users.Update(existUser);
        await dbContext.SaveChangesAsync();
        var response = mapper.ToReadDto(existUser);
        return response;
    }

    public async Task<UserReadDto> CreateUserAsync(UserDto user)
    {
        if (string.IsNullOrEmpty(user.Name))
        {
            throw new BadRequestException(UserMessageConstant.RequiredName, ErrorCodeConstants.InvalidInput);
        }

        var userEntity = mapper.ToEntity(user);
        await dbContext.Users.AddAsync(userEntity);
        await dbContext.SaveChangesAsync();
        var response = mapper.ToReadDto(userEntity);
        return response;
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await dbContext.Users.FindAsync(userId);
        //if (user == null)
        //    throw new NotFoundException(string.Format(ErrorMessageConstants.ResourceNotFoundById,"User",userId), ErrorCodeConstants.NotFound);
        if (user != null)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}