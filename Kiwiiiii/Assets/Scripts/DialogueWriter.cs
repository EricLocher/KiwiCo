using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueWriter : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    public void Print(string[] dialogue)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        StartCoroutine(PlayText(dialogue));
    }

    IEnumerator PlayText(string[] arr)
    {
        foreach (string s in arr)
        {
            dialogueText.text = "";
            foreach (char c in s)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(0.125f);
            }
            yield return new WaitForSeconds(6f);
        }
    }

}
