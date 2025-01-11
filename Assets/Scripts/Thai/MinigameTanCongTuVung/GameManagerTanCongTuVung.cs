using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerTanCongTuVung : GameManager
{
    public static GameManagerTanCongTuVung Instance;

    [SerializeField] private float spawnImageCd;

    private List<Vocabulary> wrongVocaList = new List<Vocabulary>();
    private List<Sprite> allVocaSpriteOnARound = new List<Sprite>();
    private List<Sprite> vocasSpriteRemainOnARound = new List<Sprite>();
    private List<string> allVocaTextOnARound = new List<string>();
    private List<string> vocasTextRemainOnARound = new List<string>();
    private int allVocaNum = 0;
    private int spawnedNum = 0;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    public override void OnSelectDifficulty(int diff)
    {
        base.OnSelectDifficulty(diff);
        allVocaSpriteOnARound.Clear();
        allVocaTextOnARound.Clear();
        switch (diff)
        {
            case 0:
                timer = 15f;
                allVocaNum = 7;
                currentVocabulary = VocabularyManager.instance.GetRandomEasyVocabulary();
                for (int i = 0; i < allVocaNum; i++)
                {
                    allVocaSpriteOnARound.Add(VocabularyManager.instance.GetRandomEasyVocaSprite(currentVocabulary.image));
                    allVocaTextOnARound.Add(VocabularyManager.instance.GetRandomEasyVocaText(currentVocabulary.vocabulary));
                }
                break;
            case 1:
                timer = 20f;
                allVocaNum = 10;
                currentVocabulary = VocabularyManager.instance.GetRandomMediumVocabulary();
                for (int i = 0; i < allVocaNum; i++)
                {
                    allVocaSpriteOnARound.Add(VocabularyManager.instance.GetRandomMediumVocaSprite(currentVocabulary.image));
                    allVocaTextOnARound.Add(VocabularyManager.instance.GetRandomMediumVocaText(currentVocabulary.vocabulary));
                }
                break;
            case 2:
                timer = 25f;
                allVocaNum = 13;
                currentVocabulary = VocabularyManager.instance.GetRandomHardVocabulary();
                for (int i = 0; i < allVocaNum; i++)
                {
                    allVocaSpriteOnARound.Add(VocabularyManager.instance.GetRandomHardVocaSprite(currentVocabulary.image));
                    allVocaTextOnARound.Add(VocabularyManager.instance.GetRandomHardVocaText(currentVocabulary.vocabulary));
                }
                break;
        }
        int rd = Random.Range(0, allVocaNum);
        allVocaSpriteOnARound[rd] = currentVocabulary.image;
        rd = Random.Range(0, allVocaNum);
        allVocaTextOnARound[rd] = currentVocabulary.vocabulary;
        vocasSpriteRemainOnARound = new List<Sprite>(allVocaSpriteOnARound);
        vocasTextRemainOnARound = new List<string>(allVocaTextOnARound);
        InvokeRepeating("SpawnVocaSprite", 2f, 2f);
        InvokeRepeating("SpawnVocaText", 2f, 2f);
    }
    protected override void ResetGameState()
    {
        base.ResetGameState();
        wrongVocaList.Clear();
        spawnedNum = 0;
    }
    private void GetNewVocasSpriteListForNewRound()
    {
        vocasSpriteRemainOnARound.Clear();
        List<int> indexOfvocasRemain = new List<int>();
        for (int i = 0; i < allVocaNum; i++)
        {
            indexOfvocasRemain.Add(i);
        }
        while (indexOfvocasRemain.Count >= 1)
        {
            int rdIndex = Random.Range(0, indexOfvocasRemain.Count);
            vocasSpriteRemainOnARound.Add(allVocaSpriteOnARound[rdIndex]);
            indexOfvocasRemain.Remove(indexOfvocasRemain[rdIndex]);
        }
    }
    private void GetNewVocasTextListForNewRound()
    {
        vocasTextRemainOnARound.Clear();
        List<int> indexOfvocasRemain = new List<int>();
        for (int i = 0; i < allVocaNum; i++)
        {
            indexOfvocasRemain.Add(i);
        }
        while (indexOfvocasRemain.Count >= 1)
        {
            int rdIndex = Random.Range(0, indexOfvocasRemain.Count);
            vocasTextRemainOnARound.Add(allVocaTextOnARound[rdIndex]);
            indexOfvocasRemain.Remove(indexOfvocasRemain[rdIndex]);
        }
    }
    public Sprite GetSpriteFromVocaList()
    {
        Sprite result = vocasSpriteRemainOnARound[spawnedNum];
        spawnedNum++;
        if (spawnedNum >= allVocaNum)
        {
            GetNewVocasSpriteListForNewRound();
            spawnedNum = 0;
        }
        return result;
    }
    public string GetTextFromVocaList()
    {
        string result = vocasTextRemainOnARound[spawnedNum];
        spawnedNum++;
        if (spawnedNum >= allVocaNum)
        {
            GetNewVocasTextListForNewRound();
            spawnedNum = 0;
        }
        return result;
    }
    private void SpawnVocaSprite()
    {
        ImageGameplaySpawner.Instance.SpawnVocaImage();
    }
    private void SpawnVocaText()
    {
        ImageGameplaySpawner.Instance.SpawnVocaText();
    }
}
