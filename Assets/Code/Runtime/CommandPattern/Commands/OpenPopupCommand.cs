using CodingTest.Runtime.Provider;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class OpenPopupCommand : BaseCommand
    {
        public string popupText { get; private set; }
        public static event Action<string> OnShowPopup;

        public OpenPopupCommand(string popupText) => this.popupText = popupText;
        public OpenPopupCommand(OpenPopupMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            OnShowPopup?.Invoke(popupText);

            ReplayProvider.Instance.Record(Serialize());
        }

        public override CommandMemento Serialize() => new OpenPopupMemento(new OpenPopupCommand(popupText));
        public override void Deserialize(CommandMemento memento) => popupText = (memento as OpenPopupMemento).PopupText;
    }

    [DataContract]
    public class OpenPopupMemento : CommandMemento
    {
        [DataMember] public readonly string PopupText;
        public OpenPopupMemento(OpenPopupCommand command) : base(command) => PopupText = command.popupText;
    }
}
