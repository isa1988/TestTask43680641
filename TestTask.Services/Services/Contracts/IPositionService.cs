using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Core.DataBase;
using TestTask.Services.Dto;

namespace TestTask.Services.Services.Contracts
{
    public interface IPositionService : IGeneralService<Position, PositionDto, Guid>
    {
    }
}
