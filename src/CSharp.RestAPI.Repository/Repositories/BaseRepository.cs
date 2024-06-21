using CSharp.RestAPI.Repository.Controllers;
using CSharp.RestAPI.Repository.DataContext;

namespace CSharp.RestAPI.Repository.Repositories
{
    public class BaseRepository
    {
        protected ILogger<BaseRepository> log;
        protected TemporaryDataContext dataContext;
    }
}
