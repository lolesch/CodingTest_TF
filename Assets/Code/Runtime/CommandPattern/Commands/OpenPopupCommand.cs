using CodingTest.Runtime.Provider;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class OpenPopupCommand : BaseCommand
    {
        public string PopupText { get; private set; }

        public static event Action<string> OnShowPopup;

        public OpenPopupCommand(string popupText) => PopupText = popupText;
        public OpenPopupCommand(OpenPopupMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            OnShowPopup?.Invoke(PopupText);

            ReplayProvider.Instance.AddRecording(Serialize());
        }

        public override CommandMemento Serialize() => new OpenPopupMemento(new OpenPopupCommand(PopupText));
        public override void Deserialize(CommandMemento memento) => PopupText = (memento as OpenPopupMemento).PopupText;
    }

    [DataContract]
    public sealed class OpenPopupMemento : CommandMemento
    {
        [DataMember] public readonly string PopupText;
        public OpenPopupMemento(OpenPopupCommand command) : base(command) => PopupText = command.PopupText;
    }
}
