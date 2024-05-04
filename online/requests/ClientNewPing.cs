using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2.online.requests
{
    public class ClientNewPing : BaseRequest
    {
        public string SessionId { get; set; }
        public int Ping {  get; set; }

        public ClientNewPing(string sessionId, int ping)
        {
            SessionId = sessionId;
            Ping = ping;
        }
    }
}
