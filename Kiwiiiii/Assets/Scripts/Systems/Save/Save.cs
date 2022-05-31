using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    [HideInInspector] public int sceneIndex;
     public bool aquiredSword;
    [HideInInspector] public float master = 1f, music = 1f, sfx = 1f, sensitivity = 5f;

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
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex > 0 && buildIndex < 4)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SaveAll();
        }
    }

    public void SaveAll()
    {
        SaveData.Save(this);
    }

    public int CheckPreviousSave()
    {
        GameData data = SaveData.Load();
        return data.sceneIndex;
    }

    public void LoadSavedScene()
    {
        string sceneString = NameFromIndex(sceneIndex);
        LevelLoader.Instance.LoadLoading(sceneString);
    }

    static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    public void ResetData()
    {
        sensitivity = 5;
        sceneIndex = 0;
        aquiredSword = false;
        SaveAll();
    }

    public void LoadAll()
    {
        GameData data = SaveData.Load();
        sceneIndex = data.sceneIndex;
        aquiredSword = data.aquiredSword;
    }

    public void LoadAllSettings()
    {
        GameData data = SaveData.Load();
        sensitivity = data.sensitivity;
        master = data.master;
        sfx = data.sfx;
        music = data.music;
        AudioManager.instance.SetMasterVolume(master);
        AudioManager.instance.SetSfxVolume(sfx);
        AudioManager.instance.SetMusicVolume(music);
    }
}