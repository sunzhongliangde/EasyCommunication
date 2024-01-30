
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCommunication.DataFormat
{
    public class ClientReportDataFormat
    {
        public ClientReportDataFormat(byte[] data, long offset, long size)
        {
            var functionCode = data[0];
        }

    }
}
