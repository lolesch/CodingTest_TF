using CodingTest.Data.Enums;
using CodingTest.Runtime.Provider;
using CodingTest.Runtime.UI.Buttons;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class CycleColorCommand : BaseCommand
    {
        public RecordableButton Receiver { get; private set; }

        public CycleColorCommand(RecordableButton receiver) => Receiver = receiver;
        public CycleColorCommand(CycleColorMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            CycleColor();

            ReplayProvider.Instance.AddRecording(Serialize());
        }

        private void CycleColor() => Receiver.CurrentTint = Receiver.CurrentTint switch
        {
            ButtonTint.Red => ButtonTint.Green,
            ButtonTint.Green => ButtonTint.Blue,
            ButtonTint.Blue => ButtonTint.Red,

            _ => ButtonTint.Red
        };

        public override CommandMemento Serialize() => new CycleColorMemento(new CycleColorCommand(Receiver));
        public override void Deserialize(CommandMemento memento) => Receiver = ReplayProvider.Instance.GetButton((memento as CycleColorMemento).ButtonIndex);

    }
    [DataContract]
    public class CycleColorMemento : CommandMemento
    {
        [DataMember] public readonly int ButtonIndex;

        public CycleColorMemento(CycleColorCommand command) : base(command) => ButtonIndex = ReplayProvider.Instance.GetButtonIndex(command.Receiver);
    }
}
