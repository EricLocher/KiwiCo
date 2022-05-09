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
    [SerializeField] GameObject pauseScreen, dialogueBox, interactNotice, blackbars, levelLoader;
    [SerializeField] Animator BG;
    bool showSettings = true;

    void Start()
    {
        foreach (Transform child in transform)
            uiElements.Add(child.gameObject);

        sensSlider.value = cam.sensitivity;
        sensText.text = "Sensitivity: " + sensSlider.value;
    }

    public void CallPause()
    {
        GameController.Instance.PauseGame();
    }

    public void OnPause(GameStates state)
    {
        if (state == GameStates.Paused)
        {
            foreach (GameObject obj in uiElements)
                obj.SetActive(false);

            pauseScreen.SetActive(true);
            levelLoader.SetActive(true);
        }
        else if (state != GameStates.Paused)
        {
            foreach (GameObject obj in uiElements)
                obj.SetActive(true);

            interactNotice.SetActive(false);
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
        levelLoader.SetActive(true);
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

    public void OpenSettings()
    {
        if (showSettings)
        {
            BG.Play("OpenMore");
            showSettings = false;
        }
        if (!showSettings)
        {
            BG.Play("CloseMore");
            showSettings = true;
        }
    }

    void OnEnable() => GameController.onStateChange += OnPause;
    void OnDisable() => GameController.onStateChange -= OnPause;

}
