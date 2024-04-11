using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    class TranspostitionEntry
    {
        public int _i { get; set; }
        public int _transpositionResult { get; set; }
        public int _maxNotes { get; set; }

        public TranspostitionEntry(int i, int transpositionResult, int maxNotes)
        {
            _i = i;
            _transpositionResult = transpositionResult;
            _maxNotes = maxNotes;
        }
    }
}
