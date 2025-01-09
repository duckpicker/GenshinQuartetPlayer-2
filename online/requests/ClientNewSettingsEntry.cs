using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2.online.requests
{
    public class ClientNewSettingsEntry : BaseRequest
    {
        public int Transposition { get; set; }
        public Instrument Instrument { get; set; }
        public HashSet<int> MutedTrackChunks { get; set; }
        public int NewPing { get; set; }
        public List<string> TrackChunksString { get; set; }

        public int? UkuleleChordChanell = null;

        public ClientNewSettingsEntry(int transposition, Instrument instrument, HashSet<int> mutedTrackChunks, int newPing, List<string> trackChunksString, int? ukuleleChordChanell)
        {
            Transposition = transposition;
            Instrument = instrument;
            MutedTrackChunks = mutedTrackChunks;
            NewPing = newPing;
            TrackChunksString = trackChunksString;
            UkuleleChordChanell = ukuleleChordChanell;
        }
    }
}
