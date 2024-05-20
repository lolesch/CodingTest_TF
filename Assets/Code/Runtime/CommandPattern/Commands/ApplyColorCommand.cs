using CodingTest.Data;
using CodingTest.Data.Enums;
using CodingTest.Runtime.Provider;
using CodingTest.Runtime.UI.Buttons;
using DG.Tweening;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ApplyColorCommand : BaseCommand
    {
        public RecordableButton Receiver { get; private set; }

        public ApplyColorCommand(RecordableButton receiver) => Receiver = receiver;
        public ApplyColorCommand(ApplyColorMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            ApplyColor();

            ReplayProvider.Instance.AddRecording(Serialize());
        }

        private void ApplyColor()
        {
            var color = Receiver.CurrentTint switch
            {
                ButtonTint.Red => Constants.ButtonRed,
                ButtonTint.Green => Constants.ButtonGreen,
                ButtonTint.Blue => Constants.ButtonBlue,

                _ => Receiver.targetGraphic.color
            };

            _ = Receiver.targetGraphic.DOColor(color, 0.3f).SetEase(Ease.InOutSine);
        }

        public override CommandMemento Serialize() => new ApplyColorMemento(this);
        public override void Deserialize(CommandMemento memento)
        {
            var m = memento as ApplyColorMemento;
            Receiver = ReplayProvider.Instance.GetButton(m.ButtonIndex);
            Receiver.CurrentTint = m.Tint;
        }
    }

    [DataContract]
    public sealed class ApplyColorMemento : CommandMemento
    {
        [DataMember] public readonly int ButtonIndex;
        [DataMember] public readonly ButtonTint Tint;

        public ApplyColorMemento(ApplyColorCommand command) : base(command)
        {
            ButtonIndex = ReplayProvider.Instance.GetButtonIndex(command.Receiver);
            Tint = command.Receiver.CurrentTint;
        }
    }
}
