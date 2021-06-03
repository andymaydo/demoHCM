using System.Threading.Tasks;

namespace HCMApi
{
    public interface ICMSEvent
    {
        Task<CMSEvent> Load(string _eventID);
    }
}