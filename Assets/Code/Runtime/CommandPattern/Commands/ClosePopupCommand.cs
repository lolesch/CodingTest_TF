using CodingTest.Runtime.Provider;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ClosePopupCommand : BaseCommand
    {
        public static event Action OnHidePopup;

        public ClosePopupCommand() { }
        public ClosePopupCommand(ClosePopupMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            OnHidePopup?.Invoke();

            ReplayProvider.Instance.AddRecording(Serialize());
        }

        public override CommandMemento Serialize() => new ClosePopupMemento(this);
        public override void Deserialize(CommandMemento memento) { }
    }

    [DataContract]
    public sealed class ClosePopupMemento : CommandMemento
    {
        public ClosePopupMemento(ClosePopupCommand command) : base(command) { }
    }
}
