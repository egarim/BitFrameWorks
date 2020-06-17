using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data
{
    public static class DotNetTime
    {
        public static UInt64 ToDotNetTime(DateTime Date)
        {
            double totalMilliseconds = (Date - new DateTime(2002, 2, 13)).TotalMilliseconds;
            double value = Math.Round(totalMilliseconds, 0);
            return Convert.ToUInt64(value);
        }
        public static UInt64 DotNetNow()
        {
            return ToDotNetTime(DateTime.UtcNow);
        }
        public static UInt64 DotNetNow(DateTime Date)
        {
            return ToDotNetTime(Date);
        }
        public static UInt64 DotNetNowSmall(DateTime Date)
        {
            return Convert.ToUInt64(DotNetNow(Date) *0.5);
        }
        public static UInt64 DotNetPico(DateTime Date)
        {
            return Convert.ToUInt64(DotNetNow(Date) * 0.001);
        }
        public static UInt64 DotNetPico()
        {
            return Convert.ToUInt64(DotNetNow(DateTime.UtcNow) * 0.001);
        }
        public static UInt64 DotNetNowSmall()
        {
            return Convert.ToUInt64(DotNetNow());
        }
    }
   
}
