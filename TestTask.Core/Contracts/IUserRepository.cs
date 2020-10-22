using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Core.DataBase;

namespace TestTask.Core.Contracts
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        bool IsFullNameHereTable(string fullName, Guid? id = null);

        Task<List<User>> UsersOfPositionAsynce(Guid positionId);
    }
}
