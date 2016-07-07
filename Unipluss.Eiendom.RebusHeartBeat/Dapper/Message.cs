﻿using System;

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

    }
}