using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

namespace CodingTest.Data.ReplaySystem
{
    public sealed class Recording : MonoBehaviour
    {
        public Recording(string fileName) => this.fileName = fileName;

        // TODO: listen for inputField onEndEdit to set fileName
        [field: SerializeField] private string fileName = string.Empty;
        [field: SerializeField] private static float startedRecordingTime = 0;
        [field: SerializeField] private bool IsRecording { get; set; }
        [field: SerializeField] private bool IsPlaying { get; set; }
        [field: SerializeField] private static readonly List<RecordingEntry> entries = new();
        [field: SerializeField] private GameState CurrentState { get; set; }

        private void OnDisable() => StartRecordingCommand.OnStartRecording -= StartRecording;

        private void OnEnable()
        {
            StartRecordingCommand.OnStartRecording -= StartRecording;
            StartRecordingCommand.OnStartRecording += StartRecording;
        }

        public static void AddEntry(ICommand command) =>
            entries.Add(new RecordingEntry(Time.time - startedRecordingTime, command));

        private void StartRecording()
        {
            if (fileName == string.Empty)
            {
                Debug.LogError("File name cannot be empty");

                return;
            }

            startedRecordingTime = Time.time;
            IsRecording = true;

            // disable inputField and PlayButton

            // toggle to stopRecordingButton

            AddEntry(null);
        }

        private void StopRecording()
        {
            AddEntry(null);

            IsRecording = false;

            // save recording
            SaveGameState(fileName);
        }

        private IEnumerator PlayRecording()
        {
            // load recording
            var startedPlayingTime = Time.time;

            var i = 0;

            while (i < entries.Count)
            {
                yield return new WaitForFixedUpdate();

                var timePassed = Time.time - startedPlayingTime;

                if (entries[i].timestamp < timePassed)
                {
                    entries[i].command?.Execute();
                    i++;
                }
            }
        }

        // stop / pause playing
        private void StopPlaying() => IsPlaying = false;

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

