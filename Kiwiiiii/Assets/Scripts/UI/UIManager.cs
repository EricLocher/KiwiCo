using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider sensSlider, masterSlider, sfxSlider, musicSlider;
    [SerializeField] TMP_Text sensText;
    [SerializeField] CameraController cam;
    [SerializeField] GameObject pauseScreen, dialogueBox, interactNotice, blackbars;
    [SerializeField] Animator BG;

    void Start()
    {
        Save.instance.LoadAllSettings();
        sensSlider.value = Save.instance.sensitivity;
        masterSlider.value = Save.instance.master;
        sfxSlider.value = Save.instance.sfx;
        musicSlider.value = Save.instance.music;
        sensText.text = "" + Mathf.Round(sensSlider.value * 100.0f) * 0.01f;

        //Temporary for playtest
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            AudioManager.instance.PauseAllSound();
            AudioManager.instance.Play("Menu Music");
        }

        if(SceneManager.GetActiveScene().name == "amelie 2" || SceneManager.GetActiveScene().name == "Eric 2")
        {
            AudioManager.instance.PauseAllSound();
            AudioManager.instance.Play("Game Music");
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
        GameController.Instance.SetTime(true);
        LevelLoader.Instance.LoadLoading(sceneName);
    }

    public void OnPause(GameStates state)
    {
        if (state == GameStates.Paused)
        {
            foreach (Transform child in gameObject.transform)
                child.gameObject.SetActive(false);

            pauseScreen.SetActive(true);
        }
        else if (state != GameStates.Paused)
        {
            foreach (Transform child in gameObject.transform)
                child.gameObject.SetActive(true);

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
        Save.instance.sensitivity = sensSlider.value;
        sensText.text = "" + Mathf.Round(sensSlider.value * 100.0f) * 0.01f;
        Save.instance.sensitivity = sensSlider.value;
        Save.instance.SaveAll();
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
        Save.instance.master = masterSlider.value;
        Save.instance.SaveAll();
    }

    public void SetSfV()
    {
        AudioManager.instance.SetSfxVolume(sfxSlider.value);
        Save.instance.sfx = sfxSlider.value;
        Save.instance.SaveAll();
    }

    public void SetMuV()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
        Save.instance.music = musicSlider.value;
        Save.instance.SaveAll();
    }

    void OnEnable() => GameController.onStateChange += OnPause;
    void OnDisable() => GameController.onStateChange -= OnPause;

}
