using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    class Transposition
    {

        private const int _leftBottomNoteNumber = 48;
        private const int _rightTopLyreNoteNumber = 83;
        private const int _rightTopOldLyreNoteNumber = 82;
        private const int _rightTopWarldhownTopNoteNumber = 71;
        private const int _octave = 12;

        public Transposition()
        {

        }
        public int Transpose(int note)
        {
            note += Settings.Transposition;
            if (Settings.Instrument != Instrument.Drum)
            {
                if (note < _leftBottomNoteNumber)
                {
                    while (note <= _leftBottomNoteNumber) note += _octave;
                    return note;
                }
                switch (Settings.Instrument)
                {
                    case Instrument.Waldhorn:
                        {
                            while (note > _rightTopWarldhownTopNoteNumber) note -= _octave;
                            return note;
                        }
                    case Instrument.OldLyre:
                        {
                            while (note > _rightTopOldLyreNoteNumber) note -= _octave;
                            return note;
                        }
                    case Instrument.LyreZither:
                        {
                            while (note > _rightTopLyreNoteNumber) note -= _octave;
                            return note;
                        }
                }

            }
            else note += _octave;
            return note;
        }

    }
}
