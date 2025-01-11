using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageGameplaySpawner : Spawner
{
    public static ImageGameplaySpawner Instance;
    public string vocaTextImageName = "VocaText";
    public string vocaSpriteImageName = "VocaImage";
    [SerializeField] private Vector2 imageSpawnPos;
    [SerializeField] private Vector2 textSpawnPos;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public void SpawnVocaImage()
    {
        Transform vocaImage = Spawn(vocaSpriteImageName, imageSpawnPos, Quaternion.identity);
        vocaImage.Find("Image").GetComponent<Image>().sprite = GameManagerTanCongTuVung.Instance.GetSpriteFromVocaList();
    }
    public void SpawnVocaText()
    {
        Transform vocaText = Spawn(vocaTextImageName, textSpawnPos, Quaternion.identity);
        vocaText.Find("Text").GetComponent<TextMeshProUGUI>().text = GameManagerTanCongTuVung.Instance.GetTextFromVocaList();
    }
}
