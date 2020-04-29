using System;

namespace Migrations
{
    internal class CommandArgumentsAttribute : Attribute
    {
        public CommandArgumentsAttribute(params string[] arguments)
        {
            this.Arguments = arguments;
        }

        public string[] Arguments { get; }
    }
}
