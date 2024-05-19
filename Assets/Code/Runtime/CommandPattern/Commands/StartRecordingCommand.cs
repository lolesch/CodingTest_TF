using System;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class StartRecordingCommand : ICommand
    {
        public static event Action OnStartRecording;

        public StartRecordingCommand() { }

        public void Execute() => OnStartRecording?.Invoke();//ActionRecording.AddEntry(this);
    }
}
