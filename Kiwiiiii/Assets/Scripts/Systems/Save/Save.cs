using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    [HideInInspector]
    public int sceneIndex;
    public float master = 1f, music = 1f, sfx = 1f, sensitivity = 5f;

    public static Save instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SaveAll();
        }
    }

    public void SaveAll()
    {
        SaveData.Save(this);
    }

    public void LoadAll()
    {
        GameData data = SaveData.Load();
        sceneIndex = data.sceneIndex;
        sensitivity = data.sensitivity;
        master = data.master;
        sfx = data.sfx;
        music = data.music;
        SceneManager.LoadScene(sceneIndex);
        AudioManager.instance.SetMasterVolume(master);
        AudioManager.instance.SetSfxVolume(sfx);
        AudioManager.instance.SetMusicVolume(music);

        Debug.Log("scene: " + sceneIndex);
        Debug.Log("sens: " + sensitivity);
        Debug.Log("master: " + master);
        Debug.Log("sfx: " + sfx);
        Debug.Log("music: " + music);
    }
}
