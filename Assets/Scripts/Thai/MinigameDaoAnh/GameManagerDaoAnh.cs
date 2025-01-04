using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerDaoAnh : GameManager
{
    public static GameManagerDaoAnh Instance;
    public Action onPlayerTouchingAction;
    public Action onInitializeLv;
    [SerializeField] private List<Vocabulary> currentVocabularies = new List<Vocabulary>();
    [SerializeField] private TextMeshProUGUI currentVocaText;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject); 
        else
            Instance = this;
        onInitializeLv += SpawnNewRandomVoca;
    }
    public void OnTouching() => onPlayerTouchingAction?.Invoke();
    private void InitializeLv() => onInitializeLv?.Invoke();
    public override void OnSelectDifficulty(int diff)
    {
        base.OnSelectDifficulty(diff);
        InitializeLv();
    }
    public override void OnClickNextLv()
    {
        base.OnClickNextLv();
        SpawnNewRandomVoca();
        RacoonSpawner.Instance.SpawnRacoonOnInitializeLv();
    }
    protected override void Update()
    {
        base.Update();
        currentVocaText.text = currentVocabulary.vocabulary;
    }
    private void SpawnNewRandomVoca()
    {
        currentVocabularies.Clear();
        switch(DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                for(int i=0; i<5; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomEasyVocabulary());
                }
                break;
            case Difficulty.normal:
                for (int i = 0; i < 6; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomMediumVocabulary());
                }
                break;
            case Difficulty.hard:
                for (int i = 0; i < 7; i++)
                {
                    currentVocabularies.Add(VocabularyManager.instance.GetRandomHardVocabulary());
                }
                break;
        }
        currentVocabulary = currentVocabularies[0];
    }
    public Sprite GetImageForRacoon(int num)
    {
        return currentVocabularies[num].image;
    }
}
