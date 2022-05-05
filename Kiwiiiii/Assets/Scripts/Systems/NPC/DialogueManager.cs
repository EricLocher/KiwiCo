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
    public delegate void OnDoneDelegate();
    public event OnDoneDelegate OnDone;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        sentances = new Queue<string>();
        IsOpen = false;
    }

    public void StartDialogue(Dialogue dialogue, OnDoneDelegate onDoneDelegate)
    {
        OnDone = onDoneDelegate;
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
        string[] array = sentance.Split(' ');
        dialogueText.text = array[0];
        for (int i = 1; i < array.Length; ++i)
        {
            yield return new WaitForSeconds(0.2f);
            dialogueText.text += " " + array[i];
        }
    }

    public void EndDialogue()
    {
        IsOpen = false;
        animator.SetBool("IsOpen", IsOpen);
        OnDone?.Invoke();
        OnDone = null;
    }
}
