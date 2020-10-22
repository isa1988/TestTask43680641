using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.Services.Dto
{
    public class PositionDto: IServiceDto<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
