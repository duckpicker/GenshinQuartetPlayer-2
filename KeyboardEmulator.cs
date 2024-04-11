using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using WindowsInput;
using WindowsInput.Native;

namespace GenshinQuartetPlayer2
{
    class KeyboardEmulator
    {
        private static Transposition _transposition;
        private static InputSimulator _inputSimulator;

        private static int _drumChanel = 9;
        public KeyboardEmulator()
        {
            _transposition = new Transposition();
            _inputSimulator = new InputSimulator();
        }

        public void Emulator(MidiEvent midiEvent)
        {
            if (midiEvent is NoteOffEvent)
            {
                var noteOffEvent = midiEvent as NoteOffEvent;

                var transposisionNote = _transposition.Transpose(noteOffEvent.NoteNumber);
                if (Settings._instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transposisionNote) && noteOffEvent.Channel != _drumChanel) _inputSimulator.Keyboard.KeyUp(noteToKeyWaldhorn[transposisionNote]);

            }
            if (midiEvent is NoteOnEvent)
            {
                var noteOnEvent = midiEvent as NoteOnEvent;
                var transposisionNote = _transposition.Transpose(noteOnEvent.NoteNumber);
                if (Settings._instrument == Instrument.Lyre_Zither && noteToKeyLyre.ContainsKey(transposisionNote) && noteOnEvent.Channel != _drumChanel) _inputSimulator.Keyboard.KeyPress(noteToKeyLyre[transposisionNote]);
                if (Settings._instrument == Instrument.OldLyre && noteToKeyOldLyre.ContainsKey(transposisionNote) && noteOnEvent.Channel != _drumChanel) _inputSimulator.Keyboard.KeyPress(noteToKeyOldLyre[transposisionNote]);
                if (Settings._instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transposisionNote) && noteOnEvent.Channel != _drumChanel) _inputSimulator.Keyboard.KeyDown(noteToKeyWaldhorn[transposisionNote]);
                if (Settings._instrument == Instrument.Drum && noteToKeyDrum.ContainsKey(transposisionNote) && noteOnEvent.Channel == _drumChanel) _inputSimulator.Keyboard.KeyPress(noteToKeyDrum[transposisionNote]);
            }

        }

        public void Emulator(int note, int velocity)
        {
            if (velocity != 0)
            {
                var transpositionNote = _transposition.Transpose(note);
                if (Settings._instrument == Instrument.Lyre_Zither && noteToKeyLyre.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyLyre[transpositionNote]);
                if (Settings._instrument == Instrument.OldLyre && noteToKeyOldLyre.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyOldLyre[transpositionNote]);
                if (Settings._instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyDown(noteToKeyWaldhorn[transpositionNote]);
                if (Settings._instrument == Instrument.Drum && noteToKeyDrum.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyDrum[transpositionNote]);
            }
            else
            {
                var transposisionNote = _transposition.Transpose(note);
                if (Settings._instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transposisionNote)) _inputSimulator.Keyboard.KeyDown(noteToKeyWaldhorn[transposisionNote]);
            }

        }

        public List<TranspostitionEntry> GetBestTranspostion(MidiReader midi)
        {
            int minimumTransposition = -11;
            int maximumTransposition = 12;
            int result = 0;
            int maximumNotes = 0;

            List<TranspostitionEntry> transpostition = new List<TranspostitionEntry>();

            for (int i = minimumTransposition; i < maximumTransposition; i++)
            {
                foreach (var note in midi._midiFile.GetNotes())
                {
                    if (note.Channel != _drumChanel)
                    {
                        maximumNotes++;
                        if (Settings._instrument == Instrument.Lyre_Zither && noteToKeyLyre.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings._transposition))) result++;
                        if (Settings._instrument == Instrument.OldLyre && noteToKeyOldLyre.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings._transposition))) result++;
                        if (Settings._instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings._transposition))) result++;
                    }
                    if (Settings._instrument == Instrument.Drum && note.Channel == _drumChanel)
                    {
                        maximumNotes++;
                        if (noteToKeyDrum.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings._transposition))) result++;
                    }
                }
                TranspostitionEntry transpostitionEntry = new TranspostitionEntry(i, result, maximumNotes);
                transpostition.Add(transpostitionEntry);
                result = 0;
                maximumNotes = 0;
            }
            return transpostition;
        }

        private static Dictionary<int, VirtualKeyCode> noteToKeyLyre = new()
        {
            {48, VirtualKeyCode.VK_Z},
            {50, VirtualKeyCode.VK_X},
            {52, VirtualKeyCode.VK_C},
            {53, VirtualKeyCode.VK_V},
            {55, VirtualKeyCode.VK_B},
            {57, VirtualKeyCode.VK_N},
            {59, VirtualKeyCode.VK_M},

            {60, VirtualKeyCode.VK_A},
            {62, VirtualKeyCode.VK_S},
            {64, VirtualKeyCode.VK_D},
            {65, VirtualKeyCode.VK_F},
            {67, VirtualKeyCode.VK_G},
            {69, VirtualKeyCode.VK_H},
            {71, VirtualKeyCode.VK_J},

            {72, VirtualKeyCode.VK_Q},
            {74, VirtualKeyCode.VK_W},
            {76, VirtualKeyCode.VK_E},
            {77, VirtualKeyCode.VK_R},
            {79, VirtualKeyCode.VK_T},
            {81, VirtualKeyCode.VK_Y},
            {83, VirtualKeyCode.VK_U}
        };

        private static Dictionary<int, VirtualKeyCode> noteToKeyOldLyre = new()
        {
            {48, VirtualKeyCode.VK_Z},
            {50, VirtualKeyCode.VK_X},
            {51, VirtualKeyCode.VK_C},
            {53, VirtualKeyCode.VK_V},
            {55, VirtualKeyCode.VK_B},
            {57, VirtualKeyCode.VK_N},
            {58, VirtualKeyCode.VK_M},

            {60, VirtualKeyCode.VK_A},
            {62, VirtualKeyCode.VK_S},
            {63, VirtualKeyCode.VK_D},
            {65, VirtualKeyCode.VK_F},
            {67, VirtualKeyCode.VK_G},
            {69, VirtualKeyCode.VK_H},
            {70, VirtualKeyCode.VK_J},

            {72, VirtualKeyCode.VK_Q},
            {73, VirtualKeyCode.VK_W},
            {75, VirtualKeyCode.VK_E},
            {77, VirtualKeyCode.VK_R},
            {79, VirtualKeyCode.VK_T},
            {80, VirtualKeyCode.VK_Y},
            {82, VirtualKeyCode.VK_U}
        };

        private static Dictionary<int, VirtualKeyCode> noteToKeyWaldhorn = new()
        {
            {48, VirtualKeyCode.VK_Z},
            {50, VirtualKeyCode.VK_X},
            {52, VirtualKeyCode.VK_C},
            {53, VirtualKeyCode.VK_V},
            {55, VirtualKeyCode.VK_B},
            {57, VirtualKeyCode.VK_N},
            {59, VirtualKeyCode.VK_M},

            {60, VirtualKeyCode.VK_A},
            {62, VirtualKeyCode.VK_S},
            {64, VirtualKeyCode.VK_D},
            {65, VirtualKeyCode.VK_F},
            {67, VirtualKeyCode.VK_G},
            {69, VirtualKeyCode.VK_H},
            {71, VirtualKeyCode.VK_J}
        };

        private static Dictionary<int, VirtualKeyCode> noteToKeyDrum = new()
        {
            {48, VirtualKeyCode.VK_S}, //kick
            {50, VirtualKeyCode.VK_A}, //snare
            {51, VirtualKeyCode.VK_A}, //snare

            {47, VirtualKeyCode.VK_S}, //kick
            {52, VirtualKeyCode.VK_A}, //snare


            {53, VirtualKeyCode.VK_K}, //low toms
            {55, VirtualKeyCode.VK_K},
            {57, VirtualKeyCode.VK_K},
            
            {59, VirtualKeyCode.VK_L}, //high toms
            {61, VirtualKeyCode.VK_L},
            {62, VirtualKeyCode.VK_L},
        };
    }
}
