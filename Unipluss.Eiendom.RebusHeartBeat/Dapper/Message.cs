using System;

namespace Unipluss.Eiendom.RebusHeartBeat.Dapper
{
    public class Message
    {
        public DateTime TimeStamp { get; set; }
        public string MachineName { get; set; }
        public string Info { get; set; }
        public string Os { get; set; }
        public string UAUrl { get; set; }
        public string UAVersion { get; set; }
        public bool RebusAlive { get; set; }
        public bool SqlOk { get; set; }
        public bool UniSqlOk { get; set; }
        public bool V3Ok { get; set; }
        public bool RedisOk { get; set; }
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