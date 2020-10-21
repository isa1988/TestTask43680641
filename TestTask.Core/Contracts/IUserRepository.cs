using System;
using TestTask.Core.DataBase;

namespace TestTask.Core.Contracts
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        bool IsFullNameHereTable(string fullName);
    }
}
