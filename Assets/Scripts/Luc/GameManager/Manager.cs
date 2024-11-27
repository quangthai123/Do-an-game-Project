
using System.IO;
using UnityEngine;


public class Manager : MonoBehaviour
{
    private string filePath;
    public playerData _playerData;
    void Start()
    {
        filePath = "D:\\project unity\\du an\\Do-an-game-Project\\Assets\\Data\\savegame.json";
       Debug.Log("File will be saved at: " + Application.persistentDataPath);
    }

    public void SaveGame()
    {
        GameData data = new GameData
        {
            exp = _playerData.GetExpValue(),
            level = _playerData.GetLevel(),
            maxExp = _playerData.GetExpMax(),
        };

        string json = JsonUtility.ToJson(data);
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(json);
        }

        Debug.Log("Game Saved!");
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

}
