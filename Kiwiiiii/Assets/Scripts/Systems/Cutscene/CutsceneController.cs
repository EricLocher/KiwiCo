using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    public Queue<string> sentances;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Animator dialogueBox;
    public bool isOpen;

    void Start()
    {
        sentances = new Queue<string>();
        isOpen = false;
    }

    public void InitStart(string sentence)
    {
        StartCoroutine(StartText(sentence));
    }

    public IEnumerator StartText(string sentence)
    {
        yield return new WaitForSeconds(2f);
        isOpen = true;
        SetAnim(isOpen);
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        nameText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            nameText.text += letter;

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetAnim(bool value)
    {
        dialogueBox.SetBool("IsOpen", value);
    }
}
