using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandTC
{
    /// <summary>
    /// A stack of commands (used to undo)
    /// </summary>
    public class CommandStack
    {
        Stack<ICommand> history = new Stack<ICommand>();

        public void Execute(ICommand command)
        {
            command.Execute();
            history.Push(command);
        }

        public void Undo(ICommand command)
        {
            history.Pop().Undo();
        }
    }
}
