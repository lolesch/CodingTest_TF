using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

namespace CodingTest.Data.ReplaySystem
{
    public sealed class Recording : ISerializable<Recording.Memento>
    {
        [field: SerializeField] private static float startedRecordingTime = 0;

        public List<RecordingEntryMemento> RecordedCommands;

        public Recording()
        {
            startedRecordingTime = Time.time;
            RecordedCommands = new List<RecordingEntryMemento>();
        }

        public Recording(Memento memento) => Deserialize(memento);

        public void AddEntry(CommandMemento commandMemento)
        {
            RecordedCommands ??= new List<RecordingEntryMemento>();

            RecordedCommands.Add(new(Time.time - startedRecordingTime, commandMemento));
        }

        public void Save(string fileName)
        {
            var serializer = new DataContractJsonSerializer(typeof(Recording.Memento));

            var stream = new MemoryStream();

            var memento = Serialize();

            if (stream == null || memento == null)
            {
                Debug.LogError($"Something was null: {stream} / {memento}");
                return;
            }

            serializer.WriteObject(stream, memento);

            SerialisationProvider.Instance.Save(Constants.SaveDirectory, fileName, stream);
        }

        public static Recording Load(string fileName)
        {
            var stream = SerialisationProvider.Instance.Load(Constants.SaveDirectory, fileName);

            //loadedStateFromFile = true;

            if (stream == null)
                return new Recording();

            var serializer = new DataContractJsonSerializer(typeof(Recording.Memento));

            // try catch...
            var memento = (Recording.Memento)serializer.ReadObject(stream);

            // => OnLoadingCompleted?.Invoke();

            return new Recording(memento);
        }

        public Memento Serialize() => new(startedRecordingTime, RecordedCommands);

        public void Deserialize(Memento memento)
        {
            startedRecordingTime = memento.StartedRecordingTime;

            RecordedCommands = memento.EntryMementos;
        }

        [DataContract]
        public sealed class Memento : AbstractMemento
        {
            [DataMember] public float StartedRecordingTime;

            [DataMember] public List<RecordingEntryMemento> EntryMementos;

            public Memento(float startedRecordingTime, List<RecordingEntryMemento> entries)
            {
                StartedRecordingTime = startedRecordingTime;
                EntryMementos = entries;
            }
        }
    }
}
