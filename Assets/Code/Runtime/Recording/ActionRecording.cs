using CodingTest_TF.Runtime.CommandPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodingTest_TF.Data.Recordings
{
    public sealed class ActionRecording
    {
        private bool IsRecording { get; set; }
        private bool IsPlaying { get; set; }

        private readonly List<RecordingEntry> entries;

        private void AddEntry(float timestamp, ICommand command) =>
            entries.Add(new RecordingEntry(timestamp, command));

        private void StartRecording() => AddEntry(Time.time, null);

        private void StopRecording()
        {
            AddEntry(Time.time, null);

            // OrderByDecending
            // this allows reverse lookup with removal without reordering the entire list
            entries.Sort((a, b) => b.timestamp.CompareTo(a.timestamp));
        }

        // save recording

        // load recording

        // play recording

        private IEnumerator PlayRecording()
        {
            _ = Time.time;

            for (var i = entries.Count; i-- > 0;)
            {

                entries[i].command?.Execute();
                yield return null;
            }
        }
    }

    public readonly struct RecordingEntry
    {
        public readonly float timestamp;
        public readonly ICommand command;

        public RecordingEntry(float timestamp, ICommand command)
        {
            this.timestamp = timestamp;
            this.command = command;
        }
    }
}

