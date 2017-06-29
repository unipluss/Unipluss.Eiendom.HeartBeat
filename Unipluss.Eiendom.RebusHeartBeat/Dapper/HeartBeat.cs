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
        public string Universion { get; set; }
        public string Sql_server_version { get; set; }
        public string Disk { get; set; }
        public string CpuThisProcess { get; set; }
        public string CpuTotal { get; set; }
        public string AvailableRam { get; set; }
        public bool UaRebusOk { get; set; }
        public int AntallBoliger { get; set; }
        public int AntallBoligerInaktive { get; set; }
    }
}