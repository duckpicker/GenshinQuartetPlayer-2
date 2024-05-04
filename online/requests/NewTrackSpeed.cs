using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2.online.requests
{
    public class NewTrackSpeed : BaseRequest
    {
        public decimal Speed { get; set; }

        public NewTrackSpeed(decimal speed)
        {
            this.Speed = speed;
        }
    }
}
