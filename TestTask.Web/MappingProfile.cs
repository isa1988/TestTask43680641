using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestTask.Services.Dto;
using TestTask.Web.Model.Position;
using TestTask.Web.Model.User;

namespace TestTask.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            UserMapping();
            PositionMapping();
        }

        private void UserMapping()
        {
            CreateMap<UserCreateModel, UserDto>();
            CreateMap<UserEditModel, UserDto>();
            CreateMap<UserDto, UserEditModel>();
            CreateMap<UserDto, UserModel>();
        }

        private void PositionMapping()
        {
            CreateMap<PositionCreateModel, PositionDto>();
            CreateMap<PositionEditModel, PositionDto>();
            CreateMap<PositionDto, PositionEditModel>();
            CreateMap<PositionDto, PositionModel>();
        }
    }
}
