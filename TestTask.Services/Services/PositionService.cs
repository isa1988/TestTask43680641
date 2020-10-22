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
    public class PositionService : GeneralService<Position, PositionDto, Guid>, IPositionService
    {
        private readonly IPositionRepository positionRepository;
        public PositionService(IMapper mapper, IPositionRepository repository) : base(new PositionDto(), mapper, repository)
        {
            positionRepository = extendedRepositoryBase as IPositionRepository;
        }

        protected override string CheckBeforeModification(PositionDto value, bool isNew = true)
        {
            string errors = string.Empty;
            if (string.IsNullOrWhiteSpace(value.Name))
                errors += "Не заполнено наименование";
            if (isNew)
            {
                if (positionRepository.IsNameHereTable(value.Name))
                    errors += "В базе уже есть это наименование";
            }
            else
            {
                if (positionRepository.IsNameHereTable(value.Name, value.Id))
                    errors += "В базе уже есть это наименование";
            }

            return errors;
        }

        protected override string CkeckBeforeDelete(Position value)
        {
            return string.Empty;
        }
    }
}
