using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private string dialogue1, dialogue2, dialogue3, dialogue4;

    private bool interactable, finished;

    void Start()
    {
        interactable = finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 5f && !finished)
        {
            interactable = true;
        }
        else
        {
            interactable = false;
        }

        if (interactable)
            PrintText();
    }

    void PrintText()
    {
        finished = true;

        Debug.Log(dialogue1);
    }
}
