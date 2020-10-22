using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.Services.Dto
{
    public class UserDto: IServiceDto<Guid>
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public Guid PositionId { get; set; }
        public PositionDto Position { get; set; }
    }
}
