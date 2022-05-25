using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public static GameStates gameState = GameStates.Menu;

    [SerializeField] InputAction pauseGame;
    [SerializeField] Texture2D cursorTexture;

    public delegate void ChangeHandler(GameStates state);
    public static event ChangeHandler onStateChange;

    [HideInInspector] public bool pause;

    float saveTimer = 0f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Cursor.SetCursor(cursorTexture, new Vector2(0, 0), CursorMode.ForceSoftware);

        pauseGame.Enable();

        pauseGame.performed += ctx => PauseGame();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            var pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            pc.stats.health = pc.stats.maxHealth;
        }

        if (buildIndex == 0 || buildIndex >= 4) { gameState = GameStates.Menu; }
        if (buildIndex > 0 && buildIndex < 4) { gameState = GameStates.Playing; }
        if (buildIndex == 6) { gameState = GameStates.Loading; }

        if(gameState == GameStates.Playing) { }
    }

    void Update()
    {
        if (gameState == GameStates.Playing) { pause = true; } else { pause = false; }

        if (gameState == GameStates.Playing)
        {
            saveTimer += Time.deltaTime;
            if (saveTimer >= 30)
            {
                Save.instance.SaveAll();
                saveTimer = 0;
            }
        }
    }

    public void PauseGame()
    {
        if(gameState == GameStates.Menu || gameState == GameStates.Loading) { return; }
        if (Instance.pause)
        {
            AudioManager.instance.PlayOnce("Pause");
            SetTime(false);
            gameState = GameStates.Paused;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            AudioManager.instance.PlayOnce("Unpause");
            SetTime(true);
            gameState = GameStates.Playing;
            onStateChange?.Invoke(gameState);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        onStateChange?.Invoke(gameState);
    }

    public void Quit()
    {
        Save.instance.SaveAll();
#if UNITY_EDITOR
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void SetTime(bool scale)
    {
        if (!scale)
            Time.timeScale = 0f;

        if (scale)
            Time.timeScale = 1f;
    }
}

public enum GameStates
{
    Paused,
    Playing,
    Loading,
    Menu
}
