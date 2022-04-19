using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    public Queue<string> sentances;
    public TextMeshProUGUI dialogueText;
    public Animator dialogueBox;
    public string sentance;

    void Start()
    {
        sentances = new Queue<string>();
    }

    public IEnumerator StartText()
    {
        yield return new WaitForSeconds(4f);
        dialogueBox.SetBool("IsOpen", true);
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
}
