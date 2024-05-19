namespace CodingTest.Runtime.Provider
{
    public sealed class ReplayProvider : AbstractProvider<ReplayProvider>
    {
        //private readonly Recording recording = new(fileName: "Replay");

        public string RecordingName = string.Empty;

        public void SetRecordingName(string name) => RecordingName = name;
    }
}
