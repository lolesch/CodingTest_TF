using CodingTest.Runtime.Provider;

namespace CodingTest.Runtime.UI.Toggles
{
    public sealed class StartStopReplayToggle : AbstractToggle
    {
        protected override void Toggle(bool isOn)
        {
            if (isOn)
                ReplayProvider.Instance.StartReplaying();
            else
                ReplayProvider.Instance.StopReplaying();
        }

        // TODO: make it event based instead of update
        private void FixedUpdate()
        {
            interactable = ReplayProvider.Instance.RecordingName != string.Empty && !ReplayProvider.Instance.IsRecording;

            if (IsOn && !ReplayProvider.Instance.IsReplaying)
                SetToggle(false);
        }
    }
}
