using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Console
{
    private readonly string prefix;
    private readonly IEnumerable<DebugCommand> commands;

    public Console(string prefix, IEnumerable<DebugCommand> commands)
    {
        this.prefix = prefix;
        this.commands = commands;
    }

    public void ProcessCommand(string commandInput)
    {
        if(!commandInput.StartsWith(prefix)) { return; }

        commandInput = commandInput.Remove(0, prefix.Length);

        string[] split = commandInput.Split(' ');
        commandInput = split[0];

        string[] args = split.Skip(1).ToArray();


        foreach (var command in commands) {
            if(!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase)) {
                continue;
            }

            if (command.Process(args)) {
                return;
            }
        }
    }

}
