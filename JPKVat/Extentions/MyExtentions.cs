using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPKVat.Extentions
{
    public static class MyExtentions
    {
        public static string ToJPK(this DateTime datetime)
        {
            return string.Format("{0:d4}-{1:d2}-{2:d2}", datetime.Year, datetime.Month, datetime.Day);
        }

        public static string ToJPKDataUtworzenia(this DateTime datetime)
        {
            return string.Format("{0:d4}-{1:d2}-{2:d2}T{3:d2}:{4:d2}:{5:d2}", datetime.Year, datetime.Month, datetime.Day,datetime.Hour,datetime.Minute,datetime.Second);
        }

    }
}
