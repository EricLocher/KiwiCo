using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace Quest.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI aiText;
        [SerializeField] Button nextButton;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] GameObject aiResponse;


        private void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(NextDialogue);
            UpdateDialogueUI();
        }

        private void NextDialogue()
        {
            playerConversant.NextDialogue();
            UpdateDialogueUI();
        }
        private void UpdateDialogueUI()
        {
            aiResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
            if(playerConversant.IsChoosing())
            {
                foreach (Transform item in choiceRoot)
                {
                    Destroy(item.gameObject);
                }
                foreach (DialogueNode choiceNode in playerConversant.GetChoices())
                {
                    GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                    var textComponent = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                    textComponent.text = choiceNode.GetText();
                }
            }          
            else
            {
                aiText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNextDialogue());
            }
        }
    }
}