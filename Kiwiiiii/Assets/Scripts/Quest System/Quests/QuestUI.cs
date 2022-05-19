using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] SOQuest quest;

    private void Start()
    {
        foreach (string task in quest.GetTasks())
        {
            Debug.Log($"To do: {task}.");
        }
    }
}