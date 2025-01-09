using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using WindowsInput;
using WindowsInput.Native;

namespace GenshinQuartetPlayer2
{
    class KeyboardEmulator
    {
        private Transposition _transposition;
        private InputSimulator _inputSimulator;



        private const int _drumChanel = 9;
        public KeyboardEmulator()
        {
            _transposition = new Transposition();
            _inputSimulator = new InputSimulator();
        }

        public void GetInstrumentEmulator(MidiEvent midiEvent, Instrument instrument)
        {
            if (midiEvent is NoteOnEvent)
            {
                var noteOnEvent = midiEvent as NoteOnEvent;
                var transposisionNote = _transposition.Transpose(noteOnEvent.NoteNumber, instrument);
                //Console.WriteLine($"{instrument.ToString()} {transposisionNote}");
                if (noteOnEvent.Channel == _drumChanel && (Settings.Instrument == Instrument.Drum || Settings.Instrument == Instrument.JamJamDrum))
                {
                    if (Settings.Instrument == Instrument.Drum)
                    {
                        if (noteToKeyDrum.ContainsKey(transposisionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyDrum[transposisionNote]);
                        return;
                    }
                    if (Settings.Instrument == Instrument.JamJamDrum)
                    {
                        if (noteToKeyJamJamDrum.ContainsKey(transposisionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyJamJamDrum[transposisionNote]);
                        return;
                    }
                }
                else if (instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transposisionNote) && noteOnEvent.Channel != _drumChanel && Settings.Instrument == Instrument.Waldhorn)
                {
                    _inputSimulator.Keyboard.KeyDown(noteToKeyWaldhorn[transposisionNote]);
                    return;
                }
                else if (InstrumentDictionary.ContainsKey(instrument) && noteOnEvent.Channel != _drumChanel) { if (InstrumentDictionary[instrument].ContainsKey(transposisionNote)) _inputSimulator.Keyboard.KeyPress(InstrumentDictionary[instrument][transposisionNote]); }
            }
            if (midiEvent is NoteOffEvent)
            {
                var noteOffEvent = midiEvent as NoteOffEvent;

                var transposisionNote = _transposition.Transpose(noteOffEvent.NoteNumber, Settings.Instrument);
                if (Settings.Instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transposisionNote) && noteOffEvent.Channel != _drumChanel) _inputSimulator.Keyboard.KeyUp(noteToKeyWaldhorn[transposisionNote]);
            }
        }

        public void Emulator(int note, int velocity)
        {
            //if (velocity != 0)
            //{
            //    var transpositionNote = _transposition.Transpose(note);
            //    if (Settings.Instrument == Instrument.LyreZither && noteToKeyLyre.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyLyre[transpositionNote]);
            //    if (Settings.Instrument == Instrument.OldLyre && noteToKeyOldLyre.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyOldLyre[transpositionNote]);
            //    if (Settings.Instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyDown(noteToKeyWaldhorn[transpositionNote]);
            //    if (Settings.Instrument == Instrument.Drum && noteToKeyDrum.ContainsKey(transpositionNote)) _inputSimulator.Keyboard.KeyPress(noteToKeyDrum[transpositionNote]);
            //}
            //else
            //{
            //    var transposisionNote = _transposition.Transpose(note);
            //    if (Settings.Instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(transposisionNote)) _inputSimulator.Keyboard.KeyDown(noteToKeyWaldhorn[transposisionNote]);
            //}

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
                foreach (var note in midi.MidiFile.GetNotes())
                {
                    if (note.Channel != _drumChanel)
                    {
                        maximumNotes++;
                        if (Settings.Instrument == Instrument.LyreZither && noteToKeyLyre.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings.Transposition, Instrument.LyreZither))) result++;
                        if (Settings.Instrument == Instrument.OldLyre && noteToKeyOldLyre.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings.Transposition, Instrument.OldLyre))) result++;
                        if (Settings.Instrument == Instrument.Waldhorn && noteToKeyWaldhorn.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings.Transposition, Instrument.Waldhorn))) result++;
                        if (Settings.Instrument == Instrument.Ukulele && noteToKeyUkulele.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings.Transposition, Instrument.Ukulele))) result++;
                    }
                    if (Settings.Instrument == Instrument.Drum && note.Channel == _drumChanel)
                    {
                        maximumNotes++;
                        if (noteToKeyDrum.ContainsKey(_transposition.Transpose(note.NoteNumber + i - Settings.Transposition, Instrument.Drum))) result++;
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

        private static Dictionary<int, VirtualKeyCode> noteToKeyUkulele = new()
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

        private static Dictionary<int, VirtualKeyCode> noteToKeyUkuleleChord = new()
        {
            {72, VirtualKeyCode.VK_Q},
            {74, VirtualKeyCode.VK_W},
            {76, VirtualKeyCode.VK_E},
            {77, VirtualKeyCode.VK_R},
            {79, VirtualKeyCode.VK_T},
            {81, VirtualKeyCode.VK_Y},
            {83, VirtualKeyCode.VK_U}
        };

        private static Dictionary<int, VirtualKeyCode> noteToKeyDrum = new()
        {
            {35, VirtualKeyCode.VK_S}, // kick
            {37, VirtualKeyCode.VK_A}, // snare

            {36, VirtualKeyCode.VK_S}, // kick
            {38, VirtualKeyCode.VK_A}, // snare
            {40, VirtualKeyCode.VK_A}, // snare


            {41, VirtualKeyCode.VK_K}, // low toms
            {43, VirtualKeyCode.VK_K},
            {45, VirtualKeyCode.VK_K},

            {47, VirtualKeyCode.VK_L}, // high toms
            {48, VirtualKeyCode.VK_L},
            {50, VirtualKeyCode.VK_L},

            {49, VirtualKeyCode.VK_L}, // crash
        };

        private static Dictionary<int, VirtualKeyCode> noteToKeyJamJamDrum = new()
        {
            {35, VirtualKeyCode.VK_Q}, // kick
            {37, VirtualKeyCode.VK_I}, // snare

            {36, VirtualKeyCode.VK_A}, // kick
            {38, VirtualKeyCode.VK_K}, // snare
            {40, VirtualKeyCode.VK_K}, // snare


            {41, VirtualKeyCode.VK_A}, // low toms
            {43, VirtualKeyCode.VK_Q},
            {45, VirtualKeyCode.VK_W},

            {47, VirtualKeyCode.VK_S}, // high toms
            {48, VirtualKeyCode.VK_I},
            {50, VirtualKeyCode.VK_I},

            {42, VirtualKeyCode.VK_I },
            {44, VirtualKeyCode.VK_I },
            {46, VirtualKeyCode.VK_I }, //hihats

            {49, VirtualKeyCode.VK_S}, // crash
            {51, VirtualKeyCode.VK_I}, // ride
        };

        private static Dictionary<Instrument, Dictionary<int, VirtualKeyCode>> InstrumentDictionary = new()
        {
            {Instrument.LyreZither, noteToKeyLyre },
            {Instrument.OldLyre, noteToKeyOldLyre },
            {Instrument.Ukulele, noteToKeyUkulele },
            {Instrument.UkuleleChord, noteToKeyUkuleleChord }
        };
    }
}
