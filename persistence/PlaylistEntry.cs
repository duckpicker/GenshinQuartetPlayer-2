using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    class PlaylistEntry
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
