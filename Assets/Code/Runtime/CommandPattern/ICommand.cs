namespace CodingTest_TF.Runtime.CommandPattern
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
