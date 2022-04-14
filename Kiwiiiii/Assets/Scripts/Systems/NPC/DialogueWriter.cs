using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueWriter : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float charAmount, referenceCharAmount;
    public bool dialogueFinished, inter;

    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        dialogueFinished = true;
        inter = true;
    }

    public void Print(string[] dialogue)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        StartCoroutine(PlayText(dialogue));
    }

    //rewrite sloppy code
    IEnumerator PlayText(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            foreach (char x in arr[i])
            {
                referenceCharAmount++;
            }
        }
        foreach (string s in arr)
        {
            dialogueText.text = "";
            foreach (char c in s)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(0.125f);
                charAmount++;

                if (referenceCharAmount == charAmount)
                {
                    charAmount = referenceCharAmount = 0;
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    dialogueText.gameObject.SetActive(false);
                    dialogueFinished = false;
                }
            }
            if (!inter)
                break;
            yield return new WaitForSeconds(6f);
        }
    }

}
