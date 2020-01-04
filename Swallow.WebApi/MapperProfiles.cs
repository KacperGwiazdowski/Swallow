using AutoMapper;
using Swallow.Core.Domains.User;
using Swallow.WebApi.Models;

namespace Swallow.WebApi
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password))
                .ForMember(x => x.TelephoneNumber, y => y.MapFrom(z => z.Telephone));
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto, UserRole>();
            CreateMap<User, UserLoginDto>();
        }
    }
}
