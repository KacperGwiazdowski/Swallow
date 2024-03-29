﻿using AutoMapper;
using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Domains.User;
using Swallow.WebApi.Models;

namespace Swallow.WebApi
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.IsActive, y => y.MapFrom(z => z.IsAccountActive));
            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password))
                .ForMember(x => x.TelephoneNumber, y => y.MapFrom(z => z.Telephone));
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto, UserRole>();
            CreateMap<User, UserLoginDto>();
            CreateMap<Sensor, SensorDto>();
            CreateMap<SensorDto, Sensor>();
        }
    }
}
