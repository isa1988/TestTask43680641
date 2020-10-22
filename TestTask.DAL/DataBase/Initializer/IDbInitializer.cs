using System.Threading.Tasks;

namespace TestTask.DAL.DataBase.Initializer
{
    public interface IDbInitializer
    {
        public Task Initialize();
    }
}
