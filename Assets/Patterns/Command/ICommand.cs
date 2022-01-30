
namespace CommandTC
{
    /// <summary>
    /// Interface for the Command pattern.
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
