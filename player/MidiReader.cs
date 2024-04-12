using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinQuartetPlayer2.online.requests;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace GenshinQuartetPlayer2
{
    class MidiReader : BaseRequest
    {
        private IEnumerable<MidiPlay> timedEvents { get; set; }
        private TempoMap tempoMap { get; set; }
        private MyPlayback myPlayBack { get; set; }
        
        public MidiFile MidiFile { get; private set; }
        public IEnumerable<TrackChunk> TrackChunks { get; private set; }
        public TimeSpan TotalTime { get; private set; }
        public HashSet<int> MutedTrackChunks { get; set; }

        public MidiReader(string file)
        {
            MidiFile = MidiFile.Read(file);
            TrimAdjacentNotes(Settings.TrimDurationTime);
            timedEvents = MidiFile.GetTrackChunks().SelectMany((c, i) => c.GetTimedEvents().Select(e => new MidiPlay(e.Event, e.Time, i))).OrderBy(e => e.Time);
            tempoMap = MidiFile.GetTempoMap();
            TrackChunks = MidiFile.GetTrackChunks();
            MutedTrackChunks = new HashSet<int>();
            TotalTime = MidiFile.GetTimedEvents().LastOrDefault().TimeAs<MetricTimeSpan>(tempoMap);
            myPlayBack = new MyPlayback(timedEvents, tempoMap, this);
        }

        public void TrimAdjacentNotes(int trimDurationTime)
        {
            foreach (var trackChunk in MidiFile.GetTrackChunks()) trackChunk.ProcessNotes(n => n.Length = (n.Length - trimDurationTime) < 1 ? 1 : n.Length - trimDurationTime);
        }

        public void Start(TimeSpan currentTime)
        {
            WindowFinder.Find();
            MetricTimeSpan metricTimeSpan = new MetricTimeSpan(currentTime);
            myPlayBack.Start();
            myPlayBack.Speed = (double)Settings.Speed;
            myPlayBack.MoveToTime(metricTimeSpan);
        }

        public void SwitchTraks(int trackNumber)
        {
            if (MutedTrackChunks.Contains(trackNumber)) MutedTrackChunks.Remove(trackNumber);
            else MutedTrackChunks.Add(trackNumber);
        }
    }
}
