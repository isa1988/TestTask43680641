using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TestTask.Core.Contracts;
using TestTask.Core.DataBase;
using TestTask.DAL.DataBase;

namespace TestTask.DAL.Repositories
{
    public class PositionRepository : Repository<Position, Guid>, IPositionRepository
    {
        public PositionRepository(DbContextTestTask contextSpaTestTask) 
            : base(contextSpaTestTask)
        {
        }

        protected override Guid GetNewId()
        {
            return Guid.NewGuid();
        }

        public bool IsNameHereTable(string name)
        {
            // В данной задаче SQL Lite не позволяет без учеча регистра по этому в целях побыстрее сделать и не зависаит над решением было принято решение применить ToList()
            var flag = contextSpaTestTask.Positions.ToList().Any(x => x.Name.ToUpper() == name.ToUpper());

            return flag;
        }
    }
}
