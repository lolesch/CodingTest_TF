using CodingTest.Data.Enums;
using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using CodingTest.Runtime.UI.Buttons;
using System.Runtime.Serialization;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class CycleColorCommand : ICommand, ISerializable<CycleColorCommand.Memento>
    {
        public CycleColorCommand(RecordableButton receiver) => this.receiver = receiver;

        [SerializeField] private RecordableButton receiver;

        public void Execute()
        {
            CycleColor();

            ReplayProvider.Instance.AddEntry(Serialize());
        }

        private void CycleColor() => receiver.CurrentTint = receiver.CurrentTint switch
        {
            ButtonTint.Red => ButtonTint.Green,
            ButtonTint.Green => ButtonTint.Blue,
            ButtonTint.Blue => ButtonTint.Red,

            _ => ButtonTint.Red
        };
        public Memento Serialize() => throw new System.NotImplementedException();
        public void Deserialize(Memento memento) => throw new System.NotImplementedException();

        [DataContract]
        public class Memento : AbstractICommandMemento
        {
            public Memento(ICommand command) : base(command)
            {

            }
        }
    }
}
