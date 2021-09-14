
namespace CommandTC
{
    public class NullCommand : ICommand
    {
        public void Execute() { }
        public void Undo() { }
    }
}
