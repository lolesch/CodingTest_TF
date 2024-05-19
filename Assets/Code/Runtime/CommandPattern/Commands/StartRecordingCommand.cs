using System;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class StartRecordingCommand : ICommand
    {
        public static event Action OnStartRecording;

        public StartRecordingCommand() { }

        public void Execute() => OnStartRecording?.Invoke();//ActionRecording.AddEntry(this);
    }
}
