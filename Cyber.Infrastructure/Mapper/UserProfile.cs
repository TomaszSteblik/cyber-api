using AutoMapper;
using Cyber.Domain.Entities;

namespace Cyber.Infrastructure.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, DAOs.User>().ConstructUsing(src => new DAOs.User());
        CreateMap<DAOs.User, User>().ConstructUsing(src => new User());
    }
}