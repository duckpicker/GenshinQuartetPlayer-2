using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    public class MidiPlay : TimedEvent, IMetadata
    {
        public MidiPlay(MidiEvent midiEvent, long time, int trackChunkIndex)
            : base(midiEvent, time)
        {
            Metadata = trackChunkIndex;
        }

        public object Metadata { get; set; }
    }
}
