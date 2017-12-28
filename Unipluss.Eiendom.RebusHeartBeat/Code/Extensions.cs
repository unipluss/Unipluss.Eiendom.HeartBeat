using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Ajax.Utilities;
using Unipluss.Eiendom.RebusHeartBeat.Dapper;

namespace Unipluss.Eiendom.RebusHeartBeat.Code
{
    public static class Extensions
    {

        public static void PoupulateDiskList(this Message message)
        {
            if (message.Disk.IsNullOrWhiteSpace()) return;
            if (message.Disk.ToLower().Contains("unknown"))
            {
               return;
            }
            List<string> list = new List<string>();
            message.Disk = message.Disk.Replace(@":\,", ":$");
            message.Disk = message.Disk.Replace("[",string.Empty);
            message.Disk = message.Disk.Replace("]", string.Empty);

            Dictionary<string,string> dict = message.Disk.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(part => part.Split('$'))
               .ToDictionary(split => split[0], split => split[1]);
            foreach (string item in dict.Keys)
            {
                double val = dict[item].ToDouble();
                string temp = string.Format("{0} {1}GB", item, Math.Round(val,2));
                list.Add(temp);
            }

            message.Disk = string.Join(" ",list);
        }

        public static double ToDouble(this string str)
        {
            double d = 0.0;
            if (!double.TryParse(str,out d))
            {
                return 0.0;
            }
            return d;
        }

        public static double ToDoubleNbNo(this string str)
        {
            double d = 0.0;
            if (!double.TryParse(str, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfoByIetfLanguageTag("nb-NO"), out d))
            {
                return 0.0;
            }
            return d;
        }

        public static string ToTwoDecimal(this string str)
        {
            double d = str.ToDouble();
            d = Math.Round(d, 2);
            return d.ToString();
        }

        public static string ToTwoDecimalDbNo(this string str)
        {
            double d = str.ToDoubleNbNo();
            d = Math.Round(d, 2);
            return d.ToString(CultureInfo.GetCultureInfoByIetfLanguageTag("nb-NO"));
        }
    }
}