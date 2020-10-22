using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Core.DataBase;

namespace TestTask.Core.Contracts
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        bool IsFullNameHereTable(string fullName);

        Task<List<User>> UsersOfPositionAsynce(Guid positionId);
    }
}
