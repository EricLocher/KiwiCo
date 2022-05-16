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
        [SerializeField] Button quitButton;
        [SerializeField] TextMeshProUGUI currentSpeakerName;


        private void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onDialogueUpdate += UpdateDialogueUI;
            nextButton.onClick.AddListener(() => playerConversant.NextDialogue());
            quitButton.onClick.AddListener(() => playerConversant.QuitConversing());
            UpdateDialogueUI();
        }

        private void UpdateDialogueUI()
        {
            gameObject.SetActive(playerConversant.CurrentDialogueIsActive());

            if(!playerConversant.CurrentDialogueIsActive())
            {
                return;
            }

            currentSpeakerName.text = playerConversant.GetCurrentSpeakerName();

            aiResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
            if(playerConversant.IsChoosing())
            {
                MakeChoiceList();
            }
            else
            {
                aiText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNextDialogue());
            }
        }

        private void MakeChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode playerChoice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComponent = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComponent.text = playerChoice.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    playerConversant.ChoiceSelect(playerChoice);
                });
            }
        }
    }
}