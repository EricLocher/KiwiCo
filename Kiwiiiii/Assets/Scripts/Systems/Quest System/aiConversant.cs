using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiConversant : MonoBehaviour
{
    [SerializeField] string npcName;

    public string GetName()
    {
        return npcName;
    }
    //TODO: Make dialogue pop up when player presses E
}