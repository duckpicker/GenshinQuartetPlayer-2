using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2.online.requests
{
    public class ConnectionConfirm : BaseRequest
    {
        public int PingCount { get; set; } = 0;
        public int Ping { get; set; } = 0;
        public DateTime NowDateTime { get; set; }
        public DateTime PreviousDateTime { get; set; } = DateTime.Now;

        public ConnectionConfirm(DateTime dateTime)
        {
            NowDateTime = dateTime;
        }
    }
}
