using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2.online.requests
{
    public class NewTrackTime : BaseRequest
    {
        public TimeSpan Time { get; set; }

        public NewTrackTime(TimeSpan time)
        {
            this.Time = time;
        }
    }
}
