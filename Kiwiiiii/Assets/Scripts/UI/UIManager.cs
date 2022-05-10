using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    List<GameObject> uiElements = new List<GameObject>();
    [SerializeField] Slider sensSlider, masterSlider, sfxSlider, musicSlider;
    [SerializeField] TMP_Text sensText;
    [SerializeField] CameraController cam;
    [SerializeField] GameObject pauseScreen, dialogueBox, interactNotice, blackbars;
    [SerializeField] Animator BG;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            foreach (Transform child in transform)
                uiElements.Add(child.gameObject);

            sensSlider.value = cam.sensitivity;
            sensText.text = "" + Mathf.Round(sensSlider.value * 100.0f) * 0.01f;
        }
    }

    public void CallPause()
    {
        GameController.Instance.PauseGame();
    }

    public void CallQuit()
    {
        GameController.Instance.Quit();
    }

    public void LoadScene(string sceneName)
    {
        LevelLoader.Instance.LoadLoading(sceneName);
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
        sensText.text = ""+Mathf.Round(sensSlider.value * 100.0f) * 0.01f;
    }

    public void OpenSettings()
    {
        if (BG.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) { return; }
        
        if (BG.GetCurrentAnimatorStateInfo(0).IsName("OpenMore"))
        {
            BG.SetTrigger("Close");
        }
        else
        {
            BG.SetTrigger("Open");
        }
    }

    public void SetMaV()
    {
        AudioManager.instance.SetMasterVolume(masterSlider.value);
    }

    public void SetSfV()
    {
        AudioManager.instance.SetSfxVolume(sfxSlider.value);
    }

    public void SetMuV()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }

    void OnEnable() => GameController.onStateChange += OnPause;
    void OnDisable() => GameController.onStateChange -= OnPause;

}
