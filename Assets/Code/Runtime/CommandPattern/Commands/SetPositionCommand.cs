using CodingTest.Runtime.Provider;
using CodingTest.Runtime.UI.Buttons;
using DG.Tweening;
using System.Runtime.Serialization;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class SetPositionCommand : BaseCommand
    {
        public RecordableButton Receiver { get; private set; }
        public Vector2 Position { get; private set; }

        public SetPositionCommand(RecordableButton receiver, Vector2 position)
        {
            Receiver = receiver;
            Position = position;
        }
        public SetPositionCommand(SetPositionMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            _ = Receiver.transform.DOMove(Position, Time.deltaTime).SetEase(Ease.InOutSine);

            ReplayProvider.Instance.AddRecording(Serialize());
        }

        public override CommandMemento Serialize() => new SetPositionMemento(this);

        public override void Deserialize(CommandMemento memento)
        {
            var m = memento as SetPositionMemento;
            Receiver = Receiver = ReplayProvider.Instance.GetButton(m.ButtonIndex);
            Position = m.Position;
        }
    }

    [DataContract]
    public sealed class SetPositionMemento : CommandMemento
    {
        [DataMember] public readonly int ButtonIndex = -1;
        [DataMember] public readonly Vector2 Position;

        public SetPositionMemento(SetPositionCommand command) : base(command)
        {
            ButtonIndex = ReplayProvider.Instance.GetButtonIndex(command.Receiver);
            Position = command.Position;
        }
    }
}
