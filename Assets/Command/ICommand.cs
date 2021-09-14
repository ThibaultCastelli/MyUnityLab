
namespace CommandTC
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
