using CodingTest.Data.ReplaySystem;
using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Serialization;
using CodingTest.Runtime.UI.Buttons;
using CodingTest.Utility.AttributeRefs;
using System.Collections;
using UnityEngine;

namespace CodingTest.Runtime.Provider
{
    internal sealed class ReplayProvider : AbstractProvider<ReplayProvider>
    {
        private Recording recording;

        [SerializeField, ReadOnly] private RecordableButton[] recordableButtons = new RecordableButton[3];

        internal string RecordingName = string.Empty;

        internal bool IsRecording { get; private set; }
        internal bool IsReplaying { get; private set; }

        private void Start() => recordableButtons = FindObjectsByType<RecordableButton>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        internal void SetRecordingName(string name) => RecordingName = name;

        internal void StartRecording()
        {
            if (RecordingName == string.Empty)
            {
                Debug.LogWarning("Cannot start recording without a name!");
                return;
            }
            recording = new();
            IsRecording = true;

            foreach (var button in recordableButtons)
            {
                var posCommandMemento = new SetPositionCommand(button.transform.position, button).Serialize();
                AddEntry(posCommandMemento);

                var applyColorMemento = new ApplyColorCommand(button).Serialize();
                AddEntry(applyColorMemento);
            }
        }

        internal void AddEntry(AbstractICommandMemento commandMemento)
        {
            if (recording == null || !IsRecording)
                return;

            recording.AddEntry(commandMemento);
        }

        internal void StopRecording()
        {
            if (recording == null || !IsRecording)
                return;

            AddEntry(null);

            recording.Save(RecordingName);
            IsRecording = false;
        }

        internal void StartReplaying()
        {
            recording = Recording.Load(RecordingName);
            IsReplaying = true;

            _ = StartCoroutine(PlayRecording(recording));
        }

        internal void StopReplaying() => IsReplaying = false;

        private IEnumerator PlayRecording(Recording recording)
        {
            var entries = recording.Entries;

            var startedPlayingTime = Time.time;

            var i = 0;

            while (i < entries.Count)
            {
                var timePassed = Time.time - startedPlayingTime;

                if (entries[i].timestamp <= timePassed)
                {
                    entries[i].commandMemento?.Command.Execute();
                    i++;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
