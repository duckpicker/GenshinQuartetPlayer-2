using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    class PlaylistEntry
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }

        public PlaylistEntry(int ID, string Name, string FilePath)
        {
            this.ID = ID;
            this.Name = Name;
            this.FilePath = FilePath;
        }

        public PlaylistEntry() { }
    }
}
