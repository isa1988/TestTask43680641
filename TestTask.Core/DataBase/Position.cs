using System;
using System.Collections.Generic;

namespace TestTask.Core.DataBase
{
    public class Position : IEntity<Guid>
    {
        public Position()
        {
            Users = new List<User>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
