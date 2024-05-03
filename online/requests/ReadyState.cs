using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2.online.requests
{
    public class ReadyState : BaseRequest
    {
        public string SessionId { get; set; } = "";
        public bool Ready { get; set; } = false;
    }
}
