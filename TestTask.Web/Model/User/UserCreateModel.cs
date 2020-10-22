using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Web.Model.Position;

namespace TestTask.Web.Model.User
{
    public class UserCreateModel
    {
        public string FullName { get; set; }


        public Guid PositionId { get; set; }

        public PositionModel Position { get; set; }
    }
}
