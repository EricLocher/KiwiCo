using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    List<GameObject> uiElements = new List<GameObject>();
    [SerializeField] Slider sensSlider;
    [SerializeField] TMP_Text sensText;
    [SerializeField] CameraController cam;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject interactNotice;
    [SerializeField] GameObject blackbars;

    void Start()
    {
        foreach (Transform child in transform)
            uiElements.Add(child.gameObject);

        sensSlider.value = cam.sensitivity;
        sensText.text = "Sensitivity: " + sensSlider.value;
    }

    public void OnPause(GameStates state)
    {
        if (state == GameStates.Paused)
        {
            foreach (GameObject obj in uiElements)
                obj.SetActive(false);

            pauseScreen.SetActive(true);
        }
        else if (state != GameStates.Paused)
        {
            foreach (GameObject obj in uiElements)
                obj.SetActive(true);

            pauseScreen.SetActive(false);
        }
    }

    public void HideAllUIExceptCutscene()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        dialogueBox.SetActive(true);
        blackbars.SetActive(true);
    }

    public void ShowAllUIElements()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        interactNotice.SetActive(false);
        pauseScreen.SetActive(false);
    }

    public void SensitivityChange()
    {
        cam.sensitivity = sensSlider.value;
        sensText.text = "Sensitivity: " + sensSlider.value;
    }

    void OnEnable() => GameController.onStateChange += OnPause;
    void OnDisable() => GameController.onStateChange -= OnPause;

}
