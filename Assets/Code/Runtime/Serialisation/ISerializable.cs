namespace CodingTest_TF.Runtime.Serialization
{
    public interface ISerializable<T> where T : AbstractMemento
    {
        public abstract T Serialize();
        public abstract void Deserialize(T memento);
    }
}
