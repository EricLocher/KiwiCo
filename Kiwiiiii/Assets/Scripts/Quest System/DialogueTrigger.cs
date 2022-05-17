using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Quest.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public string action;
        public UnityEvent onTrigger;
        public void Trigger(string actionToTrigger)
        {
            if(actionToTrigger == action)
            {
                onTrigger.Invoke();
            }
        }
    }
}