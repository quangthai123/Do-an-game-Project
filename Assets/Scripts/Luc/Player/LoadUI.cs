using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static Cinemachine.DocumentationSortingAttribute;

public class LoadUI : MonoBehaviour
{
    private string filePath;
    public Slider expBar;
    public TextMeshProUGUI level;
    public playerData _playerData;
    public expPlayer _expPlayer;
    void Start()
    {
        filePath = "D:\\project unity\\du an\\Do-an-game-Project\\Assets\\Data\\savegame.json";
        Debug.Log("File will be saved at: " + Application.persistentDataPath);
        LoadGame();
    }
    void Update()
    {
        updateUI();
    }
    public void LoadGame()
    {
        FileInfo fileinfo = new FileInfo(filePath);
        if (fileinfo.Exists)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                GameData data = JsonUtility.FromJson<GameData>(json);
                _playerData.expValue = data.exp;
                _playerData.levelValue = data.level;
                _playerData.expMax = data.maxExp;
                Debug.Log("game loaded! exp: " + data.exp);
                Debug.Log("game loaded! level: " + data.level);
                Debug.Log("game loaded! maxexp: " + data.maxExp);
            }
        }
        else
        {
            Debug.Log("save file not found!");

        }
    }
    public void updateUI()
    {
        expBar.value = _playerData.GetExpValue();
        level.text = "Lv." + _playerData.GetLevel().ToString();
    }
}
