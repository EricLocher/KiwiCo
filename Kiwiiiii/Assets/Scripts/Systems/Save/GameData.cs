[System.Serializable]
public class GameData
{
    public int sceneIndex;
    public int deathCounter;
    public float timePlayed;

    public GameData(Save save)
    {
        sceneIndex = save.sceneIndex;
    }
}