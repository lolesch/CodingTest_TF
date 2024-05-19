using CodingTest.Data.Enums;
using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using CodingTest.Runtime.UI.Buttons;
using CodingTest.Utility.Extensions;
using System.Runtime.Serialization;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ApplyColorCommand : ICommand, ISerializable<ApplyColorCommand.Memento>
    {
        public ApplyColorCommand(RecordableButton receiver) => this.receiver = receiver;

        [SerializeField] private RecordableButton receiver;

        public void Execute()
        {
            ApplyColor();

            ReplayProvider.Instance.AddEntry(Serialize());
        }

        private void ApplyColor() => receiver.targetGraphic.color = receiver.CurrentTint switch
        {
            ButtonTint.Red => ColorExtensions.ButtonRed,
            ButtonTint.Green => ColorExtensions.ButtonGreen,
            ButtonTint.Blue => ColorExtensions.ButtonBlue,

            _ => receiver.targetGraphic.color
        };
        public Memento Serialize() => throw new System.NotImplementedException();
        public void Deserialize(Memento memento) => throw new System.NotImplementedException();

        [DataContract]
        public class Memento : AbstractICommandMemento
        {
            [DataMember] public RecordableButton receiver;

            public Memento(ApplyColorCommand command) : base(command) => receiver = command.receiver;
        }
    }
}
