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
    public bool isOpen;

    void Start()
    {
        sentances = new Queue<string>();
<<<<<<< HEAD
        isOpen = false;
    }

    void Update()
    {
        dialogueBox.SetBool("IsOpen", isOpen);
=======

        StartCoroutine("StartText");
>>>>>>> 98458621a287f88804a3628460c0878cf136e17b
    }

    IEnumerator StartText()
    {
        yield return new WaitForSeconds(4f);
        isOpen = true;
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
