using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

namespace CodingTest.Data.ReplaySystem
{
    public class Recording : ISerializable<Recording.Memento>
    {
        [field: SerializeField] private static float startedRecordingTime = 0;

        public List<RecordingEntry> Entries;

        public Recording() => Debug.LogWarning("Created new Recording");

        public Recording(Memento memento) => Deserialize(memento);

        // the command stores the receiver, so we dont need that here
        public void AddEntry(AbstractICommandMemento commandMemento)
        {
            Entries ??= new List<RecordingEntry>();

            Entries.Add(new RecordingEntry(Time.time - startedRecordingTime, commandMemento));
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

        public Memento Serialize() => new(startedRecordingTime, Entries);
        public void Deserialize(Memento memento)
        {
            startedRecordingTime = memento.StartedRecordingTime;
            Entries = memento.Entries;
        }

        [DataContract]
        public sealed class Memento : AbstractMemento
        {
            [DataMember] public float StartedRecordingTime = 0;
            [DataMember] public List<RecordingEntry> Entries = new();

            public Memento(float startedRecordingTime, List<RecordingEntry> entries)
            {
                StartedRecordingTime = startedRecordingTime;
                Entries = entries;
            }
        }

        public readonly struct RecordingEntry
        {
            public readonly float timestamp;
            public readonly AbstractICommandMemento commandMemento;

            public RecordingEntry(float timestamp, AbstractICommandMemento commandMemento)
            {
                this.timestamp = timestamp;
                this.commandMemento = commandMemento;
            }
        }
    }
}
