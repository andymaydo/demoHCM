
namespace SendMailWs.Interfaces
{
    public interface IUnitOfWork
    {
        Task ProcessMailQueue();
    }
}