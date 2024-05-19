namespace CodingTest.Runtime.Provider
{
    internal sealed class ReplayProvider : AbstractProvider<ReplayProvider>
    {
        //private readonly Recording recording = new(fileName: "Replay");

        internal string RecordingName = string.Empty;

        internal bool IsRecording { get; private set; }
        internal bool IsReplaying { get; private set; }

        internal void SetRecordingName(string name) => RecordingName = name;
        internal void StartRecording() => IsRecording = true;
        internal void StopRecording() => IsRecording = false;

        internal void StartReplaying() => IsReplaying = true;
        internal void StopReplaying() => IsReplaying = false;
    }
}
