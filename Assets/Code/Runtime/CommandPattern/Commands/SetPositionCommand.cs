using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using CodingTest.Runtime.UI.Buttons;
using System.Runtime.Serialization;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class SetPositionCommand : ICommand, ISerializable<SetPositionCommand.Memento>
    {
        public SetPositionCommand(Vector2 position, RecordableButton receiver)
        {
            this.position = position;
            this.receiver = receiver;
        }

        [SerializeField] private Vector2 position;
        [SerializeField] private RecordableButton receiver;

        public void Execute()
        {
            receiver.transform.position = position;

            ReplayProvider.Instance.AddEntry(Serialize());
        }
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
