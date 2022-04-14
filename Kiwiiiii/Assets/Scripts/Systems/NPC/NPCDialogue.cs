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

    //rewrite sloppy code
    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        if (distance < 5f && !finished)
        {
            interactable = true;
            dialogueWriter.inter = true;
        }
        else
        {
            interactable = false;
        }

        if (interactable)
            PrintText();

        if (finished && !dialogueWriter.dialogueFinished && distance >= 5f)
            finished = dialogueWriter.dialogueFinished;

        if(distance >= 5f)
        {
            dialogueWriter.dialogueFinished = finished = false;
            dialogueWriter.charAmount = dialogueWriter.referenceCharAmount = 0;
            dialogueWriter.inter = false;
            GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(1).gameObject.SetActive(false);
        }

    }

    public void PrintText()
    {
        finished = true;

        dialogueWriter.Print(dialogue);
    }
}
