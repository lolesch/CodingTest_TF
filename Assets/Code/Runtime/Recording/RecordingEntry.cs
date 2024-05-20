using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Serialization;
using System.Runtime.Serialization;

namespace CodingTest.Data.ReplaySystem
{
    public sealed class RecordingEntry : ISerializable<RecordingEntryMemento>
    {
        public float Timestamp;
        public CommandMemento CommandMemento;

        public RecordingEntry(RecordingEntryMemento memento) => Deserialize(memento);

        public RecordingEntry(float timestamp, CommandMemento memento)
        {
            Timestamp = timestamp;
            CommandMemento = memento;
        }

        public RecordingEntryMemento Serialize() => new(Timestamp, CommandMemento);

        public void Deserialize(RecordingEntryMemento memento) => Timestamp = memento.Timestamp;//_ = memento.CommandMemento?.CommandType switch//{//    ApplyColorCommand => new ApplyColorCommand(memento.CommandMemento),//};//    .Deserialize(memento.CommandMemento);
    }

    [DataContract]
    [KnownType(typeof(ApplyColorMemento))]
    [KnownType(typeof(ClosePopupMemento))]
    [KnownType(typeof(CycleColorMemento))]
    [KnownType(typeof(HideTooltipMemento))]
    [KnownType(typeof(OpenPopupMemento))]
    [KnownType(typeof(SetPositionMemento))]
    [KnownType(typeof(ShowTooltipMemento))]
    public sealed class RecordingEntryMemento : AbstractMemento
    {
        [DataMember] public float Timestamp;
        [DataMember] public CommandMemento CommandMemento;

        public RecordingEntryMemento(float timestamp, CommandMemento commandMemento)
        {
            CommandMemento = commandMemento;
            Timestamp = timestamp;
        }
    }
}
