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
    }

    public void Continue()
    {
        Save.instance.LoadAll();
        Save.instance.LoadSavedScene();
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
        AudioManager.instance.PlayOnce("Menu Button");
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
            dialogueBox.SetActive(false);
            pauseScreen.SetActive(false);
        }
    }

    public void HideAllUIExceptCutscene()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
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
        dialogueBox.SetActive(false);
    }

    public void SensitivityChange()
    {
        Save.instance.sensitivity = sensSlider.value;
        sensText.text = "" + Mathf.Round(sensSlider.value * 100.0f) / 100;
    }

    public void HoverButton()
    {
        AudioManager.instance.PlayOnce("Button Hover");
    }

    public void OpenSettings()
    {
        if (BG.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) { return; }
        AudioManager.instance.PlayOnce("Menu Button");
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
    }

    public void SetSfV()
    {
        AudioManager.instance.SetSfxVolume(sfxSlider.value);
        Save.instance.sfx = sfxSlider.value;
    }

    public void SetMuV()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
        Save.instance.music = musicSlider.value;
    }

    void OnEnable() => GameController.onStateChange += OnPause;
    void OnDisable() => GameController.onStateChange -= OnPause;

}