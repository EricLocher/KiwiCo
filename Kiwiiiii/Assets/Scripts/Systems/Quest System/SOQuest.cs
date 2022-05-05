using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "IEnumerables Primer/Quest")]
public class SOQuest : ScriptableObject
{
    [SerializeField] string[] tasks;

    public IEnumerable<string> GetTasks()
    {
        return tasks;
    }
}
