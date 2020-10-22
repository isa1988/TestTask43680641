using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.Contracts;
using TestTask.Core.DataBase;
using TestTask.DAL.DataBase;

namespace TestTask.DAL.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(DbContextTestTask contextSpaTestTask) 
            : base(contextSpaTestTask)
        {
            DbSetInclude = dbSet.Include(x => x.Position);
        }
        protected override Guid GetNewId()
        {
            return Guid.NewGuid();
        }

        public bool IsFullNameHereTable(string fullName)
        {
            // В данной задаче SQL Lite не позволяет без учеча регистра по этому в целях побыстрее сделать и не зависаит над решением было принято решение применить ToList()
            bool flag = contextSpaTestTask.Users.ToList().Any(x => x.FullName.ToLower() == fullName.ToLower());
            return flag;
        }

        public async Task<List<User>> UsersOfPositionAsynce(Guid positionId)
        {
            var users = await contextSpaTestTask.Users.Where(x => x.PositionId == positionId).ToListAsync();
            return users;
        }
    }
}
