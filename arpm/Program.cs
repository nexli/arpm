using CommandLine;
using System;

namespace Arpm
{
    class Program
    {
        static int Main(string[] args)
        {
            return CommandLine.Parser
                .Default
                .ParseArguments<Option.Resolve, Option.Interafces, Option.Spoof>(args)
                .MapResult(
                    (Option.Interafces opts) => MapCallback<Option.Interafces, Command.Interafces>(opts),
                    (Option.Spoof opts) => MapCallback<Option.Spoof, Command.Spoof>(opts),
                    (Option.Resolve opts) => MapCallback<Option.Resolve, Command.Resolve>(opts),
                    errs => 1
                );
        }

        static int MapCallback<OPT, COM>(OPT options)
        {
            try
            {
                var command = Activator.CreateInstance(typeof(COM)) as Command.ICommand<OPT>;

                command.Option = options;

                command.Execute();
            }
            catch
            {
                return 1;
            }

            return 0;
        }
    }
}
