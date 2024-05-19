﻿using CodingTest.Runtime.Provider;

namespace CodingTest.Runtime.UI.Toggles
{
    public sealed class StartStopRecordingToggle : AbstractToggle
    {
        protected override void Toggle(bool isOn)
        {
            if (isOn)
                ReplayProvider.Instance.StartRecording();
            else
                ReplayProvider.Instance.StopRecording();
        }

        // TODO: tooltip for required fileName

        // TODO: make it event based instead of update
        private void FixedUpdate() => interactable = ReplayProvider.Instance.RecordingName != string.Empty
            && !ReplayProvider.Instance.IsReplaying;
    }
}
