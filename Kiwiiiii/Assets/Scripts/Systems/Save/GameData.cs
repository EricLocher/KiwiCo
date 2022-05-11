[System.Serializable]
public class GameData
{
    public int sceneIndex;
    public float master, music, sfx, sensitivity;

    public GameData(Save save)
    {
        sceneIndex = save.sceneIndex;
        master = save.master;
        music = save.music;
        sfx = save.sfx;
        sensitivity = save.sensitivity;
    }
}