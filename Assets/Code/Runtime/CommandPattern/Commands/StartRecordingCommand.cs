using CodingTest.Data.Enums;
using CodingTest.Runtime.Serialization;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class StartRecordingCommand : ICommand, ISerializable<StartRecordingCommand.Memento>
    {
        public static event Action OnStartRecording;

        public StartRecordingCommand() { }

        public void Execute() => OnStartRecording?.Invoke();//ActionRecording.AddEntry(this);

        public Memento Serialize() => throw new System.NotImplementedException();
        public void Deserialize(Memento memento) => throw new System.NotImplementedException();

        [DataContract]
        public class Memento : AbstractMemento
        {
            [DataMember] public ButtonTint CurrentTint;
        }
    }
}
