using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Console
{
    private readonly string prefix;
    private readonly IEnumerable<DebugCommand> commands;

    string ErrorMessage => "Something went wrong, use /help for infomation on each avaiable command.";

    public Console(string prefix, IEnumerable<DebugCommand> commands)
    {
        this.prefix = prefix;
        this.commands = commands;
    }

    public string ProcessCommand(string commandInput)
    {
        if(!commandInput.StartsWith(prefix)) { return ErrorMessage; }

        commandInput = commandInput.Remove(0, prefix.Length);

        string[] split = commandInput.Split(' ');
        commandInput = split[0];

        string[] args = split.Skip(1).ToArray();


        foreach (var command in commands) {
            if(!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase)) {
                continue;
            }

            return command.Process(args);
        }

        return ErrorMessage;
    }

    

}
