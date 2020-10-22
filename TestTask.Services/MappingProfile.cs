using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestTask.Core.DataBase;
using TestTask.Services.Dto;

namespace TestTask.Services
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            UserMapping();
            PositionMapping();
        }

        private void UserMapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Position, p => p.Ignore());
        }

        private void PositionMapping()
        {
            CreateMap<Position, PositionDto>();
            CreateMap<PositionDto, Position>()
                .ForMember(x => x.Users, p => p.Ignore());
        }
    }
}
