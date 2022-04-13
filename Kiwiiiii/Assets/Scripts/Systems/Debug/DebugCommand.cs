using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DebugCommand : ScriptableObject
{
    [SerializeField] string commandWord = "";

    public string CommandWord => commandWord;

    public abstract string Process(string[] args);
    public abstract string CommandInfo();
}


