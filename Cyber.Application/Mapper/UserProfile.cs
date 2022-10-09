using AutoMapper;
using Cyber.Application.DTOs.Read;
using Cyber.Domain.Entities;

namespace Cyber.Application.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserDto>();
    }
}