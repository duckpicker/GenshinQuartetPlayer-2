using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    class Settings
    {
        public static int Transposition = 0;
        public static Instrument Instrument = Instrument.LyreZither;
        public static readonly int TrimDurationTime = 24;
        public static decimal Speed = 1.0m;
        public static int port = 8080;
        public static MidiReader CurrentMidiFile;
    }
}
