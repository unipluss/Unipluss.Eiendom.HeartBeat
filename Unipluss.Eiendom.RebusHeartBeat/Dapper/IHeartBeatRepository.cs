using System.Collections.Generic;


namespace Unipluss.Eiendom.RebusHeartBeat.Dapper
{
    interface IHeartBeatRepository
    {
        List<HeartBeat> GetLastBeats();

        List<Message> GetDetails(int customerId);
    }
}
