using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using FirstNetMongo.Domain.Models;
using FirstNetMongo.Domain.Dtos;

namespace FirstNetMongo.Domain.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRequestDto, Users>();
        CreateMap<Users, UserResponseDto>();
    }
}
