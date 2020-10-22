using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Core.Contracts;
using TestTask.Core.DataBase;

namespace TestTask.DAL.DataBase.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory serviceScopeProvider;
        private readonly IPositionRepository positionRepository;
        private readonly IUserRepository userRepository;
        public DbInitializer(IServiceScopeFactory serviceScopeProvider, IPositionRepository positionRepository, IUserRepository userRepository)
        {
            this.serviceScopeProvider = serviceScopeProvider;
            this.positionRepository = positionRepository;
            this.userRepository = userRepository;
        }
        public async Task Initialize()
        {
            using (var serviceScope = serviceScopeProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DbContextTestTask>();
                context.Database.EnsureCreated();

                if (positionRepository.IsNameHereTable("директор"))
                    return;
                var positions = await InitializePosition();
                await InitializeUser(positions);
            }
        }

        private async Task<List<Position>> InitializePosition()
        {
            var positions = new List<Position>();
            positions.Add(new Position { Name = "Директор" });
            positions.Add(new Position { Name = "Бухгалтер" });
            positions.Add(new Position { Name = "Кассир" });

            for (int i = 0; i < positions.Count; i++)
            {
                await positionRepository.CreateAsync(positions[i]);
            }
            positionRepository.Save();

            return positions;
        }

        private async Task InitializeUser(List<Position> positions)
        {
            var users = new List<User>();
            users.Add(new User {FullName = "Денисов Нелли Богданович", PositionId = positions[0].Id});
            users.Add(new User {FullName = "Рогов Арсений Романович", PositionId = positions[1].Id});
            users.Add(new User {FullName = "Пономарёв Юстиниан Давидович", PositionId = positions[1].Id});
            users.Add(new User {FullName = "Панов Давид Валентинович", PositionId = positions[1].Id});
            users.Add(new User {FullName = "Мамонтов Зиновий Альвианович", PositionId = positions[2].Id});
            users.Add(new User {FullName = "Абрамов Валентин Владленович", PositionId = positions[2].Id});
            users.Add(new User {FullName = "Борисов Григорий Антонович", PositionId = positions[2].Id});
            users.Add(new User {FullName = "Крылов Людвиг Аркадьевич", PositionId = positions[2].Id});
            users.Add(new User {FullName = "Мухин Гордей Степанович", PositionId = positions[2].Id});
            users.Add(new User {FullName = "Сидоров Ибрагил Онисимович", PositionId = positions[2].Id});

            for (int i = 0; i < users.Count; i++)
            {
                await userRepository.CreateAsync(users[i]);
            }
            userRepository.Save();
        }
    }
}
