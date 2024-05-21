using CodingTest.Data.ReplaySystem;
using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.UI.Buttons;
using System;
using System.Collections;
using UnityEngine;

namespace CodingTest.Runtime.Provider
{
    public sealed class ReplayProvider : AbstractProvider<ReplayProvider>
    {
        private Recording recording;
        private RecordableButton[] recordableButtons = new RecordableButton[3];

        public string RecordingName { get; private set; } = string.Empty;
        public bool IsRecording { get; private set; }
        public bool IsReplaying { get; private set; }

        private void Start() => recordableButtons = FindObjectsByType<RecordableButton>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        public void SetRecordingName(string name) => RecordingName = name;

        public void StartRecording()
        {
            if (RecordingName == string.Empty)
            {
                Debug.LogWarning("Cannot start recording without a name!");
                return;
            }

            if (recording == null)
                recording = new();
            else
                recording.Reset();

            IsRecording = true;

            foreach (var button in recordableButtons)
            {
                // recording initial values
                AddRecording(new SetPositionCommand(button, button.transform.position).Serialize());
                AddRecording(new ApplyColorCommand(button).Serialize());
            }
        }

        public void AddRecording(CommandMemento commandMemento)
        {
            if (recording == null || !IsRecording)
                return;

            recording.AddEntry(commandMemento);
        }

        public void StopRecording()
        {
            AddRecording(new HideTooltipCommand().Serialize());

            recording?.Save(RecordingName);
            IsRecording = false;
        }

        public void StartReplaying()
        {
            recording = Recording.Load(RecordingName);

            _ = StartCoroutine(Replay(recording));
        }

        public void StopReplaying() => IsReplaying = false;

        private IEnumerator Replay(Recording recording)
        {
            IsReplaying = true;

            var entries = recording.RecordedCommands;
            var startedPlayingTime = Time.time;

            var i = 0;

            while (i < entries.Count && IsReplaying)
            {
                var timePassed = Time.time - startedPlayingTime;

                while (i < entries.Count && entries[i].Timestamp <= timePassed)
                {
                    if (entries[i].CommandMemento == null)
                    {
                        i++;
                        continue;
                    }

                    var command = CreateCommandFromMemento(entries[i].CommandMemento);
                    command?.Execute();

                    i++;
                }

                yield return new WaitForFixedUpdate();
            }

            StopReplaying();
        }

        private BaseCommand CreateCommandFromMemento(CommandMemento memento) => memento.CommandName switch
        {
            nameof(ApplyColorCommand) => new ApplyColorCommand(memento as ApplyColorMemento),
            nameof(ClosePopupCommand) => new ClosePopupCommand(memento as ClosePopupMemento),
            nameof(CycleColorCommand) => new CycleColorCommand(memento as CycleColorMemento),
            nameof(HideTooltipCommand) => new HideTooltipCommand(memento as HideTooltipMemento),
            nameof(OpenPopupCommand) => new OpenPopupCommand(memento as OpenPopupMemento),
            nameof(SetPositionCommand) => new SetPositionCommand(memento as SetPositionMemento),
            nameof(ShowTooltipCommand) => new ShowTooltipCommand(memento as ShowTooltipMemento),

            _ => null
        };

        public int GetButtonIndex(RecordableButton receiver) => Array.IndexOf(recordableButtons, receiver);
        public RecordableButton GetButton(int index) => recordableButtons[index];
    }
}
