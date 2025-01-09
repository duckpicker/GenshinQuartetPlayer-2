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
        public static int TrimDurationTime = 24;
        public static decimal Speed = 1.0m;
        public static int Port = 8080;
        public static bool BackgroundMidiEvents = false;
        public static bool BackgroundAllMidiEvents = false;
        public static int? UkuleleChordChanell = null;

        public static void SetUkuleleChordChanell(int chanell) { if (Instrument == Instrument.Ukulele) UkuleleChordChanell = chanell; }
    }
}
