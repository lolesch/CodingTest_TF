using CodingTest_TF.Runtime.CommandPattern;
using System.Collections.Generic;
using UnityEngine;

namespace CodingTest_TF.Data.Recordings
{
    public sealed class Recording
    {
        private readonly List<RecordingEntry> _entries;

        private void AddEntry(float timestamp, ICommand command) =>
            _entries.Add(new RecordingEntry(timestamp, command));

        // einmal alles (game state)
        private void StartRecording() => AddEntry(Time.time, null);
        private void StopRecording() => AddEntry(Time.time, null);

        // save recording

        // load recording

        // play recording
    }

    public readonly struct RecordingEntry
    {
        private readonly float timestamp;
        private readonly ICommand command;

        public RecordingEntry(float timestamp, ICommand command)
        {
            this.timestamp = timestamp;
            this.command = command;
        }
    }
}

