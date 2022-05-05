using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    public Queue<string> sentances;
    public TextMeshProUGUI dialogueText;
    public Animator dialogueBox;
    public bool isOpen;

    void Start()
    {
        sentances = new Queue<string>();
        isOpen = false;
    }

    void Update()
    {
        dialogueBox.SetBool("IsOpen", isOpen);
    }

    public void InitStart(string sentence)
    {
        StartCoroutine(StartText(sentence));
    }

    public IEnumerator StartText(string sentence)
    {
        yield return new WaitForSeconds(2f);
        isOpen = true;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
