using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    public bool IsOpen;

    private Queue<string> sentances;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        sentances = new Queue<string>();
        IsOpen = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        IsOpen = true;
        animator.SetBool("IsOpen", IsOpen);

        nameText.text = dialogue.name;

        sentances.Clear();

        foreach (string sentance in dialogue.sentances)
        {
            sentances.Enqueue(sentance);
        }

        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if (sentances.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentance = sentances.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentance(sentance));
    }

    IEnumerator TypeSentance(string sentance)
    {

        dialogueText.text = "";
        foreach (char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;

            yield return null;
        }
    }

    public void EndDialogue()
    {
        IsOpen = false;
        animator.SetBool("IsOpen", IsOpen);
    }
}
