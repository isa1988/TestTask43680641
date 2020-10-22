using System;
using TestTask.Web.Model.Position;

namespace TestTask.Web.Model.User
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }


        public Guid PositionId { get; set; }

        public PositionModel Position { get; set; }
    }
}
