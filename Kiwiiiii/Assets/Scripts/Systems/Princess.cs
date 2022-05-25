using UnityEngine;

public class Princess : MonoBehaviour
{
    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.PlayOnce("PlayerEnterPortal");
        Save.instance.ResetData();
        LevelLoader.Instance.LoadLoading("Victory");
    }
}
