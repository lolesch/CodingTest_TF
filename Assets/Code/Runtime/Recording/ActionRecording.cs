using CodingTest_TF.Runtime.CommandPattern;
using CodingTest_TF.Runtime.Provider;
using CodingTest_TF.Runtime.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

namespace CodingTest_TF.Data.Recordings
{
    public sealed class ActionRecording
    {
        private readonly string fileName = string.Empty;

        public GameState CurrentState { get; private set; }

        private bool IsRecording { get; set; }
        private bool IsPlaying { get; set; }

        private readonly List<RecordingEntry> entries;

        private void AddEntry(float timestamp, ICommand command) =>
            entries.Add(new RecordingEntry(timestamp, command));

        private void StartRecording()
        {
            if (fileName == string.Empty)
            {
                Debug.LogError("File name cannot be empty");

                return;
            }
            // disable inputField and PlayButton

            AddEntry(Time.time, null);
        }

        private void StopRecording()
        {
            AddEntry(Time.time, null);

            // OrderByDecending
            // this allows reverse lookup with removal without reordering the entire list
            entries.Sort((a, b) => b.timestamp.CompareTo(a.timestamp));

            SaveGameState(fileName);
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

        public class GameState : ISerializable<GameStateMemento>
        {
            public GameState() => Debug.LogWarning("Created new GamesState");
            public GameState(GameStateMemento memento) => Deserialize(memento);

            public void Deserialize(GameStateMemento memento) => throw new NotImplementedException();
            public GameStateMemento Serialize() => throw new NotImplementedException();
        }

        [DataContract]
        public class GameStateMemento : AbstractMemento
        {
            [DataMember] public string VersionString = string.Empty;

            public GameStateMemento(string versionString) => VersionString = versionString;
        }

        public void SaveGameState(string fileName)
        {
            var serializer = new DataContractJsonSerializer(typeof(GameStateMemento));

            var stream = new MemoryStream();

            var memento = CurrentState.Serialize();

            if (stream == null || memento == null)
            {
                Debug.LogError($"Something was null: {stream} / {memento}");
                return;
            }

            serializer.WriteObject(stream, memento);


            SerialisationProvider.Instance.Save(Constants.SaveDirectory, fileName, stream);
        }

        private GameState LoadGameState()
        {
            var stream = SerialisationProvider.Instance.Load(Constants.SaveDirectory, fileName);

            //loadedStateFromFile = true;

            if (stream == null)
                return new GameState();

            var serializer = new DataContractJsonSerializer(typeof(GameStateMemento));

            // try catch...
            var memento = (GameStateMemento)serializer.ReadObject(stream);

            // => OnLoadingCompleted?.Invoke();

            return new GameState(memento);
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

