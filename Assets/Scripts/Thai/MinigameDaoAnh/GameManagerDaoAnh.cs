using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerDaoAnh : GameManager
{
    public static GameManagerDaoAnh Instance;
    public Action onPlayerTouchingAction;
    public Action onInitializeLv;
    [SerializeField] private List<Vocabulary> currentVocabularies = new List<Vocabulary>();
    [SerializeField] private TextMeshProUGUI currentVocaText;
    [SerializeField] private List<Image> vocaPulledImagesEasy = new List<Image>();
    [SerializeField] private List<Image> vocaPulledImagesMedium = new List<Image>();
    [SerializeField] private List<Image> vocaPulledImagesHard = new List<Image>();
    private int currentVocaNum = 0;
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
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                vocaPulledImagesEasy[0].transform.parent.parent.gameObject.SetActive(true);
                break;
            case Difficulty.normal:
                vocaPulledImagesMedium[0].transform.parent.parent.gameObject.SetActive(true);
                break;
            case Difficulty.hard:
                vocaPulledImagesHard[0].transform.parent.parent.gameObject.SetActive(true);
                break;
        }
    }
    public override void OnClickNextLv()
    {
        base.OnClickNextLv();
        SpawnNewRandomVoca();
        RacoonSpawner.Instance.SpawnRacoonOnInitializeLv();
        ResetPulledImageSize();
        currentVocaNum = 0;
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
    public bool CheckImagePulled(Image image)
    {
        if (image.sprite != currentVocabulary.image)
            return false;
        return true;
    }
    public void SetNewImageAfterPulledSucess(Image image)
    {
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                vocaPulledImagesEasy[currentVocaNum - 1].sprite = image.sprite;
                vocaPulledImagesEasy[currentVocaNum - 1].rectTransform.localScale = new Vector3(.9f, .8f, 1f);
                break;
            case Difficulty.normal:
                vocaPulledImagesMedium[currentVocaNum - 1].sprite = image.sprite;
                vocaPulledImagesMedium[currentVocaNum - 1].rectTransform.localScale = new Vector3(.9f, .8f, 1f);
                break;
            case Difficulty.hard:
                vocaPulledImagesHard[currentVocaNum - 1].sprite = image.sprite;
                vocaPulledImagesHard[currentVocaNum - 1].rectTransform.localScale = new Vector3(.9f, .8f, 1f);
                break;
        }
    }
    public void SetNextVocaAfterPulledSucess()
    {
        currentVocaNum++;
        if(currentVocaNum == currentVocabularies.Count)
            return;
        currentVocabulary = currentVocabularies[currentVocaNum];
    }
    protected override void ResetGameState()
    {
        base.ResetGameState();
        currentVocaNum = 0;
    }
    private void ResetPulledImageSize()
    {
        switch(DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                foreach(Image im in vocaPulledImagesEasy)
                {
                    im.transform.localScale = new Vector2(.6f, .6f);
                }
                break;
            case Difficulty.normal:
                foreach (Image im in vocaPulledImagesMedium)
                {
                    im.transform.localScale = new Vector2(.6f, .6f);
                }
                break;
            case Difficulty.hard:
                foreach (Image im in vocaPulledImagesHard)
                {
                    im.transform.localScale = new Vector2(.6f, .6f);
                }
                break;
        }
    }
}
