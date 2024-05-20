using CodingTest.Data.ReplaySystem;
using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.UI.Buttons;
using CodingTest.Utility.AttributeRefs;
using System;
using System.Collections;
using UnityEngine;

namespace CodingTest.Runtime.Provider
{
    internal sealed class ReplayProvider : AbstractProvider<ReplayProvider>
    {
        private Recording recording;

        [SerializeField, ReadOnly] public RecordableButton[] RecordableButtons = new RecordableButton[3];

        internal string RecordingName = string.Empty;

        internal bool IsRecording { get; private set; }
        internal bool IsReplaying { get; private set; }

        private void Start() => RecordableButtons = FindObjectsByType<RecordableButton>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
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

            foreach (var button in RecordableButtons)
            {
                // might want to call the commands on that button
                new SetPositionCommand(button, button.transform.position).Execute();
                new ApplyColorCommand(button).Execute();
            }
        }

        internal void Record(CommandMemento commandMemento)
        {
            if (recording == null || !IsRecording)
                return;

            recording.AddEntry(commandMemento);
        }

        internal void StopRecording()
        {
            //if (recording == null || !IsRecording)
            //    return;

            //Record(null);

            recording?.Save(RecordingName);
            IsRecording = false;
        }

        internal void StartReplaying()
        {
            recording = Recording.Load(RecordingName);
            IsReplaying = true;

            _ = StartCoroutine(Replay(recording));
        }

        internal void StopReplaying() => IsReplaying = false;

        private IEnumerator Replay(Recording recording)
        {
            var entries = recording.RecordedCommands;

            var startedPlayingTime = Time.time;

            var i = 0;

            while (i < entries.Count)
            {
                var timePassed = Time.time - startedPlayingTime;

                if (entries[i].Timestamp <= timePassed)
                {
                    if (entries[i].CommandMemento != null)
                    {
                        // create a new command based on the corresponding name and execute it
                        BaseCommand command = entries[i].CommandMemento.CommandName switch
                        {
                            // TODO: extend this!
                            nameof(ApplyColorCommand) => new ApplyColorCommand(entries[i].CommandMemento as ApplyColorMemento),
                            nameof(ClosePopupCommand) => new ClosePopupCommand(entries[i].CommandMemento as ClosePopupMemento),
                            nameof(CycleColorCommand) => new CycleColorCommand(entries[i].CommandMemento as CycleColorMemento),
                            nameof(HideTooltipCommand) => new HideTooltipCommand(entries[i].CommandMemento as HideTooltipMemento),
                            nameof(OpenPopupCommand) => new OpenPopupCommand(entries[i].CommandMemento as OpenPopupMemento),
                            nameof(SetPositionCommand) => new SetPositionCommand(entries[i].CommandMemento as SetPositionMemento),
                            nameof(ShowTooltipCommand) => new ShowTooltipCommand(entries[i].CommandMemento as ShowTooltipMemento),

                            _ => null
                        };
                        command?.Execute();
                    }
                    i++;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        internal int GetButtonIndex(RecordableButton receiver) => Array.IndexOf(RecordableButtons, receiver);
    }
}
