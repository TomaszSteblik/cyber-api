using AutoMapper;
using Cyber.Domain.ValueObjects;

namespace Cyber.Infrastructure.Mapper;

public class UserPasswordProfile : Profile
{
    public UserPasswordProfile()
    {
        CreateMap<UserPassword, DAOs.UserPassword>().ReverseMap();
    }
}