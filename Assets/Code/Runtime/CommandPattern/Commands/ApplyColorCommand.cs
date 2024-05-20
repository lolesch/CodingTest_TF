using CodingTest.Data.Enums;
using CodingTest.Runtime.Provider;
using CodingTest.Runtime.UI.Buttons;
using CodingTest.Utility.Extensions;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ApplyColorCommand : BaseCommand
    {
        public ApplyColorCommand(RecordableButton receiver) => Receiver = receiver;

        public ApplyColorCommand(ApplyColorMemento memento) => Deserialize(memento);

        public RecordableButton Receiver { get; private set; }

        public override void Execute()
        {
            ApplyColor();

            ReplayProvider.Instance.Record(Serialize());
        }

        private void ApplyColor() => Receiver.targetGraphic.color = Receiver.CurrentTint switch
        {
            ButtonTint.Red => ColorExtensions.ButtonRed,
            ButtonTint.Green => ColorExtensions.ButtonGreen,
            ButtonTint.Blue => ColorExtensions.ButtonBlue,

            _ => Receiver.targetGraphic.color
        };

        public override CommandMemento Serialize() => new ApplyColorMemento(this);
        public override void Deserialize(CommandMemento memento)
        {
            var m = memento as ApplyColorMemento;
            Receiver = ReplayProvider.Instance.RecordableButtons[m.ButtonIndex];
            Receiver.CurrentTint = m.Tint;
        }
    }

    [DataContract]
    public class ApplyColorMemento : CommandMemento
    {
        [DataMember] public readonly int ButtonIndex;
        [DataMember] public readonly ButtonTint Tint;

        public ApplyColorMemento(ApplyColorCommand command) : base(command)
        {
            ButtonIndex = Array.IndexOf(ReplayProvider.Instance.RecordableButtons, command.Receiver);
            Tint = command.Receiver.CurrentTint;
        }
    }
}
