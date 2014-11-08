using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Rikrop.Core.Framework.Logging
{
    public static class LogRecordDataValuesExtensions
    {
        public static LogRecordDataValue[] Combine(this LogRecordDataValue[] data0, LogRecordDataValue[] data1)
        {
            Contract.Requires<ArgumentNullException>(data0 != null);
            Contract.Requires<ArgumentNullException>(data1 != null);

            var arr = new LogRecordDataValue[data0.Length + data1.Length];
            data0.CopyTo(arr, 0);
            data1.CopyTo(arr, data0.Length);
            return arr;
        }

        public static LogRecordDataValue[] Combine(this LogRecordDataValue[] data0, LogRecordDataValue[] data1, LogRecordDataValue[] data2)
        {
            Contract.Requires<ArgumentNullException>(data0 != null);
            Contract.Requires<ArgumentNullException>(data1 != null);
            Contract.Requires<ArgumentNullException>(data2 != null);

            var bytesWritten = 0;
            var arr = new LogRecordDataValue[data0.Length + data1.Length + data2.Length];
            data0.CopyTo(arr, bytesWritten);
            bytesWritten += data0.Length;
            data1.CopyTo(arr, bytesWritten);
            bytesWritten += data1.Length;
            data2.CopyTo(arr, bytesWritten);

            return arr;
        }

        public static LogRecordDataValue[] Combine(this LogRecordDataValue[] data0, params LogRecordDataValue[][] datas)
        {
            Contract.Requires<ArgumentNullException>(data0 != null);

            var datasLengthSum = datas.Sum(o => o.Length);

            var bytesWritten = 0;
            var arr = new LogRecordDataValue[data0.Length + datasLengthSum];
            
            data0.CopyTo(arr, bytesWritten);
            bytesWritten += data0.Length;

            foreach (LogRecordDataValue[] data1 in datas)
            {
                data1.CopyTo(arr, bytesWritten);
                bytesWritten += data1.Length;
            }

            return arr;
        }
    }
}