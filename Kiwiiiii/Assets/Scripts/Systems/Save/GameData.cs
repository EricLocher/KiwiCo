[System.Serializable]
public class GameData
{
    public int sceneIndex;
    public bool aquiredSword;
    public float master, music, sfx, sensitivity;

    public GameData(Save save)
    {
        sceneIndex = save.sceneIndex;
        aquiredSword = save.aquiredSword;
        master = save.master;
        music = save.music;
        sfx = save.sfx;
        sensitivity = save.sensitivity;
    }
}