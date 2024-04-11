using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace GenshinQuartetPlayer2
{
    class MidiReader
    {
        public IEnumerable<MidiPlay> _timedEvents { get; private set; }
        public TempoMap _tempoMap { get; private set; }
        public MidiFile _midiFile { get; private set; }
        public IEnumerable<TrackChunk> _trackChunks { get; private set; }
        public TimeSpan _totalTime { get; private set; }
        public List<int> _mutedTrackChunks { get; set; }
        public MyPlayback _myPlayBack { get; private set; }

        public MidiReader(string file)
        {
            _midiFile = MidiFile.Read(file);
            TrimAdjacentNotes(Settings._trimDurationTime);
            _timedEvents = _midiFile.GetTrackChunks().SelectMany((c, i) => c.GetTimedEvents().Select(e => new MidiPlay(e.Event, e.Time, i))).OrderBy(e => e.Time);
            _tempoMap = _midiFile.GetTempoMap();
            _trackChunks = _midiFile.GetTrackChunks();
            _mutedTrackChunks = new List<int>();
            for (int i = 0; i < _trackChunks.Count(); i++) _mutedTrackChunks.Add(_trackChunks.Count());
            for (int i = 0; i < _trackChunks.Count(); i++) _mutedTrackChunks[i] = _trackChunks.Count();
            _totalTime = _midiFile.GetTimedEvents().LastOrDefault().TimeAs<MetricTimeSpan>(_tempoMap);
            _myPlayBack = new MyPlayback(_timedEvents, _tempoMap, this);
        }

        public void TrimAdjacentNotes(int trimDurationTime)
        {
            foreach (var trackChunk in _midiFile.GetTrackChunks()) trackChunk.ProcessNotes(n => n.Length = (n.Length - trimDurationTime) < 1 ? 1 : n.Length - trimDurationTime);
        }

        public void Start(TimeSpan currentTime)
        {
            WindowFinder.Find();
            MetricTimeSpan metricTimeSpan = new MetricTimeSpan(currentTime);
            _myPlayBack.Start();
            _myPlayBack.Speed = (double)Settings._speed;
            _myPlayBack.MoveToTime(metricTimeSpan);
        }

        public void SwitchTraks(int id)
        {
            if (_mutedTrackChunks.Contains(id)) _mutedTrackChunks[id] = _trackChunks.Count();
            else _mutedTrackChunks[id] = id;
        }
    }
}
