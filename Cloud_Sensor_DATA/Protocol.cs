using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Sensor_DATA
{
    internal static class Protocol
    {
        public static byte[] NO2_SO2_SENSOR = new byte[] { 0x00, 0x03, 0x00, 0x02, 0x00, 0x01 };
        public static byte[] TEM_SENSOR = new byte[] { 0x00, 0x03, 0x00, 0x01, 0x00, 0x01 };
        public static byte[] HUM_SENSOR = new byte[] { 0x00, 0x03, 0x00, 0x00, 0x00, 0x01 };
    }
}
