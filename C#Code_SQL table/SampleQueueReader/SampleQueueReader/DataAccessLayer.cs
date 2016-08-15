using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleQueueReader
{
    class DataAccessLayer
    {
        public string workArea { get; set; }
        public string keyData { get; set; }
        public string lineNo { get; set; }
        public string stationName { get; set; }
        public string processName { get; set; }
        public DateTime dateTime { get; set; }
        public string scheduleID { get; set; }

    }
}
