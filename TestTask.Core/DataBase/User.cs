using System;

namespace TestTask.Core.DataBase
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public Guid PositionId { get; set; }
        public Position Position { get; set; }
    }
}
