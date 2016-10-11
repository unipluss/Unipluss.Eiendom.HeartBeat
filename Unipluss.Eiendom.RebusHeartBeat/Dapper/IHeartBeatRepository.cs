using System.Collections.Generic;

namespace Unipluss.Eiendom.RebusHeartBeat.Dapper
{
    public interface IHeartBeatRepository
    {
        List<HeartBeat> GetLastBeats();
        List<Message> GetDetails(int customerId);
    }
}