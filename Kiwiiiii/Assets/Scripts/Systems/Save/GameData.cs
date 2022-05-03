[System.Serializable]
public class GameData
{
    public int sceneIndex;

    public GameData(Save save)
    {
        sceneIndex = save.sceneIndex;
    }
}