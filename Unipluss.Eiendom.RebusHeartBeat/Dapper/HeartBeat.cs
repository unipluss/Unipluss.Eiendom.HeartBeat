using System;

namespace Unipluss.Eiendom.RebusHeartBeat.Dapper
{
    public class HeartBeat
    {
        public string CustomerName { get; set; }

        public DateTime Time { get; set; }

        public int Id { get; set; }

        public bool RebusAlive { get; set; }

        public bool SqlOk { get; set; }

        public bool UniSqlOk { get; set; }

        public bool V3Ok { get; set; }

        public bool RedisOk { get; set; }  

        public string uaUrl { get; set; }
    }
}