using System;
using TestTask.Core.DataBase;

namespace TestTask.Core.Contracts
{
    public interface IPositionRepository : IRepository<Position, Guid>
    {
        bool IsNameHereTable(string name);


    }
}
