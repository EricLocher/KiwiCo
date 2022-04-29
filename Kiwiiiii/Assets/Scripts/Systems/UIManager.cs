using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    List<GameObject> uiElements = new List<GameObject>();

    [SerializeField] GameObject pauseScreen;

    [SerializeField] Slider sensSlider;

    [SerializeField] TMP_Text sensText;

    [SerializeField] CameraController cam;

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

    public void SensitivityChange()
    {
        cam.sensitivity = sensSlider.value;
        sensText.text = "Sensitivity: " + sensSlider.value;
    }

    void OnEnable() => GameController.onStateChange += OnPause;
    void OnDisable() => GameController.onStateChange -= OnPause;

}
