using System;
using System.Linq;

namespace WinterComingGame.Game.Commands
{
    static class CommandBuilder
    {
        private const string COMMAND_PREFIX_START = "START";
        private const string COMMAND_PREFIX_JOIN = "JOIN";
        private const string COMMAND_PREFIX_SHOOT = "SHOOT";

        public static ICommand Build(string commandText)
        {
            var checkedCommand = COMMAND_PREFIX_START;
            if (commandText.StartsWith($"{checkedCommand} ", StringComparison.OrdinalIgnoreCase))
            {
                var commandArguments = commandText.Substring(checkedCommand.Length + 1);
                if (commandArguments.Any(char.IsWhiteSpace))
                {
                    throw new ArgumentException($"Invalid '{checkedCommand}' command arguments.", nameof(commandText));
                }

                return new StartGameCommand(commandArguments);
            }

            checkedCommand = COMMAND_PREFIX_JOIN;
            if (commandText.StartsWith($"{checkedCommand} ", StringComparison.OrdinalIgnoreCase))
            {
                var commandArguments = commandText.Substring(checkedCommand.Length + 1);
                if (commandArguments.Any(char.IsWhiteSpace))
                {
                    throw new ArgumentException($"Invalid '{checkedCommand}' command arguments.", nameof(commandText));
                }

                return new JoinGameCommand(commandArguments);
            }

            checkedCommand = COMMAND_PREFIX_SHOOT;
            if (commandText.StartsWith($"{checkedCommand} ", StringComparison.OrdinalIgnoreCase))
            {
                var commandArguments = commandText.Substring(checkedCommand.Length + 1).Split(' ');
                if (commandArguments.Length == 2)
                {
                    var xParsed = int.TryParse(commandArguments[0], out var x);
                    var yParsed = int.TryParse(commandArguments[1], out var y);

                    if (xParsed && yParsed)
                    {
                        return new ShootCommand(new Coordinate(x, y));
                    }
                }

                throw new ArgumentException($"Invalid '{checkedCommand}' command arguments.", nameof(commandText));
            }

            return null;
        }
    }
}
