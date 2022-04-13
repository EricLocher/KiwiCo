using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Scene Command", menuName = "Utilities/DebugCommands/SceneCommand")]
public class SceneCommand : DebugCommand
{
    public override string Process(string[] args)
    {
        if (args.Length < 1) { return CommandInfo(); }

        if (args[0] == "restart") { return "Restarting Current Level"; }
        else if (args[0] == "load") {
            if(args.Length < 2) { return CommandInfo(); }

            SceneManager.LoadScene(args[1]);
            return "";
        }

        return CommandInfo();
    }

    public override string CommandInfo()
    {
        return $"/{CommandWord} [restart/load] [name]";
    }
}
