using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private static OutputDevice _outputDevice;
        public MyPlayback(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap, MidiReader midiReader)
            : base(timedObjects, tempoMap)
        {
            _midiReader = midiReader;
            if (OutputDevice == null) _outputDevice = Melanchall.DryWetMidi.Multimedia.OutputDevice.GetByName("Microsoft GS Wavetable Synth");
        }
        protected override bool TryPlayEvent(MidiEvent midiEvent, object metadata)
        {
            if (!WindowFinder.WindowStatus())
            {
                ON_GAME_UNFOCUS?.Invoke(this, this.GetCurrentTime<MetricTimeSpan>());
                Stop();
                //_outputDevice.Dispose();
            }


            if (metadata == null) return false;
            if (_midiReader.MutedTrackChunks.Contains((int)metadata))
            {
                if (Settings.BackgroundAllMidiEvents)
                {
                    ScheduleMidiEvent(midiEvent);
                    return true;
                }
                return false;
            }

            if (Settings.BackgroundMidiEvents)
            {
                ScheduleMidiEvent(midiEvent);
            }

            _keyboardEmulator.Emulator(midiEvent);

            return true;
        }

        private void ScheduleMidiEvent(MidiEvent midiEvent)
        {
            try
            {
                var newMidiEvent = midiEvent.Clone();
                if (newMidiEvent.EventType == MidiEventType.NoteOn)
                {
                    var mid = newMidiEvent as NoteOnEvent;
                    if (mid.Channel != 9)
                    {
                        mid.NoteNumber = (SevenBitNumber)(mid.NoteNumber + Settings.Transposition);
                    }
                    _outputDevice?.SendEvent(mid);
                }
                else if (newMidiEvent.EventType == MidiEventType.NoteOff)
                {
                    var mid = newMidiEvent as NoteOffEvent;
                    if (mid.Channel != 9)
                    {
                        mid.NoteNumber = (SevenBitNumber)(mid.NoteNumber + Settings.Transposition);
                    }
                    _outputDevice?.SendEvent(mid);
                }
                else _outputDevice?.SendEvent(midiEvent);
            }
            catch (Exception ex) { }
        }

        public void DisposeDevice()
        {
            _outputDevice.Dispose();
        }
    }
}
