
namespace CommandTC
{
    /// <summary>
    /// A command that do nothing
    /// </summary>
    public class NullCommand : ICommand
    {
        public void Execute() { }
        public void Undo() { }
    }
}
