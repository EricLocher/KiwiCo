using UnityEngine;

//Ty Max för kod :)
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected bool destroyOnLoad = false;

    private static T TRUE_INSTANCE;

    public static T INSTANCE
    {
        get { return CreateIfNeededThenReturn(); }
    }

    protected virtual void Awake()
    {
        if (TRUE_INSTANCE != null) return;
        TRUE_INSTANCE = gameObject.GetComponent<T>();
        if(!destroyOnLoad)
            DontDestroyOnLoad(TRUE_INSTANCE.gameObject);
    }

    private static T CreateIfNeededThenReturn()
    {
        if (TRUE_INSTANCE == null) {
            TRUE_INSTANCE = new GameObject(typeof(T).ToString()).AddComponent<T>();
        }

        return TRUE_INSTANCE;
    }
}
