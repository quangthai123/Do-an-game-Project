using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_SXChuCai : MonoBehaviour
{
    public static GameManager_SXChuCai instance { get; private set; }
    public int currentWordLength;
    public List<Sprite> allAlphabetSprites;
    public Vocabulary currentVocabulary;
    [SerializeField] private Image vocaImage;
    [SerializeField] private TextMeshProUGUI vocaText;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    public void GetRandomEasyVocabulary()
    {
        currentVocabulary = VocabularyManager.instance.GetRandomEasyVocabulary();
        vocaImage.sprite = currentVocabulary.image;
        vocaText.text = currentVocabulary.vocabulary;
        currentWordLength = currentVocabulary.vocabulary.Length;
        PerfectWordHolder.instance.ActiveSlots();
        AlphabetHolder.instance.GetAlphabets();
    }
    public void ChangeDifficulty(int diff)
    {
        switch(diff)
        {
            case 0:
                DifficultyManager.instance.Mode = Difficulty.easy;
                break;
            case 1:
                DifficultyManager.instance.Mode = Difficulty.normal;
                break;
            case 2:
                DifficultyManager.instance.Mode = Difficulty.hard;
                break;
        }
    }
    public Sprite GetSpriteByName(char alphabet)
    {
        Debug.Log(alphabet);
        foreach(Sprite sprite in allAlphabetSprites)
        {
            if(sprite.name == alphabet.ToString())
                return sprite;
        }
        return null;
    } 
    public Sprite GetRandomAlphabetSprite()
    {
        int rdIndex = Random.Range(1, allAlphabetSprites.Count);
        return allAlphabetSprites[rdIndex];
    }
    public bool CheckAlphabet(char alphabet, int index)
    {
        return currentVocabulary.vocabulary[index] == alphabet;
    }
}
