using NAudio.Midi;

namespace GenshinQuartetPlayer2
{
    class MidiInput
    {
        private static MidiIn _midiIn;

        private static KeyboardEmulator _emulator = new KeyboardEmulator();
        private static Transposition _transposition = new Transposition();
        public static void StartInputDevice(int id)
        {
            _midiIn = new MidiIn(id);
            _midiIn.MessageReceived += MidiIn_MessageReceived;
            _midiIn.Start();
        }

        public static void StopInputDevice()
        {
            try
            {
                if (_midiIn != null)
                {
                    _midiIn.Stop();
                    _midiIn.MessageReceived -= MidiIn_MessageReceived;
                    _midiIn.Dispose();
                    _midiIn = null;
                }
            }
            catch { }
        }

        static void MidiIn_MessageReceived(object? sender, MidiInMessageEventArgs e)
        {
            try
            {
                MidiCommandCode message = e.MidiEvent.CommandCode;
                switch (message)
                {
                    case MidiCommandCode.NoteOn:
                        {
                            var noteEvent = (NoteEvent)e.MidiEvent;
                            if (!WindowFinder.WindowStatus()) break;
                            _emulator.Emulator(_transposition.Transpose(noteEvent.NoteNumber), noteEvent.Velocity);
                            break;
                        }
                }
            }
            catch (Exception exception)
            {
                if (MidiIn.NumberOfDevices > 0) StopInputDevice();
            }
        }

        public static List<string> GetInputDeviceNames()
        {
            List<string> devices = new List<string>();
            for (int i = 0; i < MidiIn.NumberOfDevices; i++) devices.Add(MidiIn.DeviceInfo(i).ProductName);
            return devices;
        }
    }
}
