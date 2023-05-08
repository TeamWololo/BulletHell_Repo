[System.Serializable]
public class PlayerData
{
    public string level;
    public float musicVolume;

    public PlayerData(string _level, float _volume)
    {
        level = _level;
        musicVolume = _volume;
    }
}
