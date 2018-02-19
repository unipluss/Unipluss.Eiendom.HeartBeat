using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Unipluss.Eiendom.RebusHeartBeat.Code;

namespace Unipluss.Eiendom.RebusHeartBeat.Dapper
{
    public class HeartBeatRepository : IHeartBeatRepository
    {
        public List<HeartBeat> GetLastBeats()
        {
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine(" SELECT DATEADD(hour,2,TimeStamp) as Time, KundeNr as Id, KundeNavn as CustomerName,");
                sql.AppendLine(" (CASE WHEN DateDiff(MINUTE,TimeStamp,GETDATE()) > 6 THEN 0 ELSE 1 END) as RebusAlive,");
                sql.AppendLine(" SqlOk, UniSqlOk, V3Ok, RedisOk, uaUrl, universion, sql_server_version, [disk], cpuThisProcess,");
                sql.AppendLine("  cpuTotal, availableRam, UaRebusOk, UaQueueServiceOk,");
                sql.AppendLine(" (CASE WHEN (SqlOk = 1 and UniSqlOk = 1 and V3Ok = 1 and RedisOk = 1 and UaRebusOk = 1)");
                sql.AppendLine("  THEN 1 ELSE 0 END) as uaOk,");
                sql.AppendLine(" (CASE WHEN (uaURL is null or uaURL = 'Missing url') THEN 1 ELSE 0 END) as uaUndefined,");
                sql.AppendLine(" 1 AS RowNumber");
                sql.AppendLine(" FROM heartbeat h where h.Id in (");
                sql.AppendLine("  select max(Id) from heartbeat ");
                sql.AppendLine("  where DateDiff(DAY,TimeStamp,GETDATE()) < 7 ");
                sql.AppendLine("  group by KundeNr )");
                sql.AppendLine(" ORDER BY RebusAlive, uaOk, uaUndefined, KundeNavn ");
                sql.AppendLine(" collate Danish_Norwegian_CI_AS");

                //string msg = ";WITH cte AS " +
                //             "(SELECT DATEADD(hour,2,TimeStamp) as Time, KundeNr as Id, KundeNavn as CustomerName, " +
                //             "(CASE WHEN DateDiff(MINUTE,TimeStamp,GETDATE()) > 6 THEN 0 ELSE 1 END) as RebusAlive, " +
                //             "SqlOk, UniSqlOk, V3Ok, RedisOk, uaUrl, universion, sql_server_version, [disk], cpuThisProcess, cpuTotal,	availableRam, UaRebusOk, UaQueueServiceOk," +
                //             "(CASE WHEN (SqlOk = 1 and UniSqlOk = 1 and V3Ok = 1 and RedisOk = 1 and UaRebusOk = 1) THEN 1 ELSE 0 END) as uaOk, " +
                //             "(CASE WHEN (uaURL is null or uaURL = 'Missing url') THEN 1 ELSE 0 END) as uaUndefined, " +
                //             "ROW_NUMBER() " +
                //             "OVER (PARTITION BY KundeNr ORDER BY [TimeStamp] DESC) " +
                //             "AS RowNumber FROM heartbeat where DateDiff(DAY,TimeStamp,GETDATE()) < 7)" +
                //             "SELECT * FROM cte WHERE RowNumber = 1" +
                //             "ORDER BY RebusAlive, uaOk, uaUndefined, CustomerName collate Danish_Norwegian_CI_AS";

                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    db.Open();

                    return db.Query<HeartBeat>(sql.ToString()).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Message> GetDetails(int customerId)
        {
            List<Message> items = null;
            try
            {
                using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
                {
                    db.Open();

                   items = db.Query<Message>("SELECT TOP 30 DATEADD(hour,2,TimeStamp) as TimeStamp, MachineName, Info, Os, " +
                                             "UAUrl, SqlOk, UniSqlOk, V3Ok, RedisOk, UAVersion, UAUrl, universion, sql_server_version, [disk], cpuThisProcess, cpuTotal, availableRam, UaRebusOk, UaQueueServiceOk," +
                                             "(CASE WHEN DateDiff(MINUTE, TimeStamp, GETDATE()) > 6 THEN 0 ELSE 1 END) as RebusAlive " +
                                             ", AntallBoliger, AntallBoligerInaktive " +
                                             " FROM HeartBeat " +
                                              "WHERE KundeNr = @custId " +
                                              "ORDER BY TimeStamp desc;", new { custId = customerId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            items.ForEach(x=> x.PoupulateDiskList());
            items.ForEach(x=> x.CpuThisProcess = x.CpuThisProcess.ToTwoDecimalDbNo());
            items.ForEach(x => x.CpuTotal = x.CpuTotal.ToTwoDecimalDbNo());
            return items;
        }
    }
}