
using Firebase.Database;
using Firebase;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public playerData _playerData;
    private DatabaseReference Database;
    private string currentUserId;

    void Start()
    {
        Database = FirebaseDatabase.GetInstance(FirebaseApp.DefaultInstance,
            "https://game-3d-english-study-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
    }

    public async void SaveGame()
    {
        currentUserId = PlayerPrefs.GetString("currentUserId");
        var updatedData = new Dictionary<string, object>
        {
            { "Exp", _playerData.expValue },
            { "MaxExp", _playerData.expMax },
            { "Level", _playerData.levelValue }
        };
        await Database.Child("user").Child(currentUserId).UpdateChildrenAsync(updatedData);
        Debug.Log("Game Saved!");
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

}
