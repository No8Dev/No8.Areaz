﻿namespace No8.Areaz.CommandLine;

[ArgsCommand("Help|?")]
public class HelpCommand : IArgsCommand
{
    public HelpCommand(string helpText, Type? commandType = null)
    {
        HelpText    = helpText;
        CommandType = commandType;
    }

    public string HelpText    { get; }
    public Type?  CommandType { get; }
}