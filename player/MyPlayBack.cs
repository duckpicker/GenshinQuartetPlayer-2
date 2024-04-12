using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    class MyPlayback : Playback
    {
        public delegate void OnGameUnfocus(object sender, TimeSpan timeSpan);
        public static event OnGameUnfocus ON_GAME_UNFOCUS;

        KeyboardEmulator _keyboardEmulator = new KeyboardEmulator();
        private MidiReader _midiReader;
        public MyPlayback(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap, MidiReader midiReader)
            : base(timedObjects, tempoMap)
        {
            _midiReader = midiReader;
        }
        protected override bool TryPlayEvent(MidiEvent midiEvent, object metadata)
        {
            if (!WindowFinder.WindowStatus())
            {
                //MainMenuForm.Instance.SetPlayTrackBarValue(this.GetCurrentTime<MetricTimeSpan>());
                ON_GAME_UNFOCUS?.Invoke(this, this.GetCurrentTime<MetricTimeSpan>());
                Stop();
            }


            if (metadata == null) return false;
            if (_midiReader.MutedTrackChunks.Contains((int)metadata)) return false;

            _keyboardEmulator.Emulator(midiEvent);
            return true;
        }
    }
}
