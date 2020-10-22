using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TestTask.Core.Contracts;
using TestTask.Core.DataBase;
using TestTask.Services.Dto;
using TestTask.Services.Services.Contracts;

namespace TestTask.Services.Services
{
    public class UserService : GeneralService<User, UserDto, Guid>, IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IMapper mapper, IUserRepository repository) : base(new UserDto(), mapper, repository)
        {
            userRepository = extendedRepositoryBase as IUserRepository;
        }

        protected override string CheckBeforeModification(UserDto value, bool isNew = true)
        {
            string errors = string.Empty;
            if (string.IsNullOrWhiteSpace(value.FullName))
                errors += "Не заполнено ФИО";
            if (isNew)
            {
                if (userRepository.IsFullNameHereTable(value.FullName))
                    errors += "В базе уже есть это ФИО";
            }
            else
            {
                if (userRepository.IsFullNameHereTable(value.FullName, value.Id))
                    errors += "В базе уже есть это ФИО";
            }

            return errors;
        }

        protected override string CkeckBeforeDelete(User value)
        {
            return string.Empty;
        }
    }
}
