

using Firebase.Database;
using Firebase;
using System.Collections.Generic;

using System.IO;
using UnityEngine;


//public class Manager : MonoBehaviour
//{
//    public playerData _playerData;
//    private DatabaseReference Database;
//    private string currentUserId;

//    void Start()
//    {
//        Database = FirebaseDatabase.GetInstance(FirebaseApp.DefaultInstance,
//            "https://game-3d-english-study-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
//    }

//    public async void SaveGame()
//    {
//        currentUserId = PlayerPrefs.GetString("currentUserId");
//        var updatedData = new Dictionary<string, object>
//        {
//            { "Exp", _playerData.expValue },
//            { "MaxExp", _playerData.expMax },
//            { "Level", _playerData.levelValue }
//        };
//        await Database.Child("user").Child(currentUserId).UpdateChildrenAsync(updatedData);
//        Debug.Log("Game Saved!");
//    }

//    public void OnApplicationQuit()

//    private string filePath;
//    public playerData _playerData;
//    void Start()
//    {
//        filePath = "D:\\project unity\\du an\\Do-an-game-Project\\Assets\\Data\\savegame.json";
//       Debug.Log("File will be saved at: " + Application.persistentDataPath);
//    }

//    public void SaveGame()
//    {
//        GameData data = new GameData
//        {
//            //exp = _playerData.GetExpValue(),
//            //level = _playerData.GetLevel(),
//            //maxExp = _playerData.GetExpMax(),
//        };

//        string json = JsonUtility.ToJson(data);
//        using (StreamWriter writer = new StreamWriter(filePath))
//        {
//            writer.Write(json);
//        }

//        Debug.Log("Game Saved!");
//    }

//    void OnApplicationQuit()
//    {
//        SaveGame();
//    }

//}
