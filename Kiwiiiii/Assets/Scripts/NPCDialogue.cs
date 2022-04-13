using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private string[] dialogue;

    private bool interactable, finished;
    private DialogueWriter dialogueWriter;
    public GameObject DialogueUI;

    void Start()
    {
        interactable = finished = false;
        dialogueWriter = DialogueUI.GetComponent<DialogueWriter>();
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

    public void PrintText()
    {
        finished = true;

        dialogueWriter.Print(dialogue);
    }
}
