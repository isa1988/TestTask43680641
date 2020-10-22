using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.Services.Dto
{
    public interface IServiceDto
    {
    }

    public interface IServiceDto<Dto> : IServiceDto where Dto : IEquatable<Dto>
    {
        Dto Id { get; set; }
    }
}
