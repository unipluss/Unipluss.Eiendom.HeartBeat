using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Unipluss.Eiendom.RebusHeartBeat.Dapper
{
    public class HeartBeatRepository : IHeartBeatRepository
    {
        public List<HeartBeat> GetLastBeats()
        {
            try
            {
                using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    db.Open();
                    var msg = ";WITH cte AS " +
                              "(SELECT DATEADD(hour,2,TimeStamp) as Time, KundeNr as Id, KundeNavn as CustomerName, " +
                              "(CASE WHEN DateDiff(MINUTE,TimeStamp,GETDATE()) > 6 THEN 0 ELSE 1 END) as RebusAlive, " +
                              "SqlOk, UniSqlOk, V3Ok, RedisOk, uaUrl," +
                              "ROW_NUMBER() " +
                              "OVER (PARTITION BY KundeNr ORDER BY [TimeStamp] DESC) " +
                              "AS RowNumber FROM heartbeat )" +
                              "SELECT * FROM cte WHERE RowNumber = 1" +
                              "ORDER BY RebusAlive, SqlOk, UniSqlOk, V3Ok, RedisOk, CustomerName collate Danish_Norwegian_CI_AS";

                    return db.Query<HeartBeat>(msg).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Message> GetDetails(int customerId)
        {
            try
            {
                using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    db.Open();

                    return db.Query<Message>("SELECT TOP 30 DATEADD(hour,2,TimeStamp) as TimeStamp, MachineName, Info, Os, " +
                                             "UAUrl, SqlOk, UniSqlOk, V3Ok, RedisOk, UAVersion, UAUrl," +
                                             "(CASE WHEN DateDiff(MINUTE, TimeStamp, GETDATE()) > 6 THEN 0 ELSE 1 END) as RebusAlive " +
                                             "FROM HeartBeat " +
                                              "WHERE KundeNr = @custId " +
                                              "ORDER BY TimeStamp desc;", new { custId = customerId }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }          
        }
    }
}