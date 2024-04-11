using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinQuartetPlayer2
{
    class TranspostitionEntry
    {
        public int Value { get; set; }
        public int TranspositionResult { get; set; }
        public int MaxNotes { get; set; }

        public TranspostitionEntry(int value, int transpositionResult, int maxNotes)
        {
            this.Value = value;
            this.TranspositionResult = transpositionResult;
            this.MaxNotes = maxNotes;
        }
    }
}
