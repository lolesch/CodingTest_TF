using CodingTest_TF.Runtime.Serialization;

namespace CodingTest_TF.UI.Buttons
{
    public sealed class ActionButton : AbstractButton, ISerializable<ActionButton.Memento>
    {

        protected override void OnLeftClick() { }

        protected override void OnRightClick() { }

        #region ISerializable
        public void Deserialize(Memento memento) => throw new System.NotImplementedException();
        public Memento Serialize() => throw new System.NotImplementedException();
        #endregion ISerializable

        public sealed class Memento : AbstractMemento
        {

        }

        //protected override void Start() => Deserialize(); // if no memento was found, use default values from scriptable object
    }
}