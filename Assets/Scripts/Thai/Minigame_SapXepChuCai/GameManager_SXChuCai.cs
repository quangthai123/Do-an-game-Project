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
    public int currentAlphabetNumOnSlot = 0;
    [Header("UI")]
    [SerializeField] private Image vocaImageGamePlay;
    [SerializeField] private Image vocaImageEndLv;
    [SerializeField] private TextMeshProUGUI vocaTextGamePlayTest;
    [SerializeField] private TextMeshProUGUI vocaTextEndLv;
    [SerializeField] private TextMeshProUGUI vocaMeaningText;
    [SerializeField] private Transform selectDiffUI;
    [SerializeField] private Transform EndLvUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private List<Transform> hearts;
    [SerializeField] private Transform blurBlackScreen;
    [SerializeField] private Transform TimeOutNoti;
    private int life = 3;
    private float timer = 0;
    private bool timeOut = false;
    private bool outOfVoca = false;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        selectDiffUI.gameObject.SetActive(true);
        EndLvUI.gameObject.SetActive(false);
    }
    public void GetRandomEasyVocabulary()
    {
        currentVocabulary = VocabularyManager.instance.GetRandomEasyVocabulary();
        if (currentVocabulary == null)
        {
            outOfVoca = true;
            return;
        }
        GetVocabulary();
    }
    public void GetRandomMediumVocabulary()
    {
        currentVocabulary = VocabularyManager.instance.GetRandomMediumVocabulary();
        GetVocabulary();
    }
    public void GetRandomHardVocabulary()
    {
        currentVocabulary = VocabularyManager.instance.GetRandomHardVocabulary();
        GetVocabulary();
    }
    private void GetVocabulary()
    {
        vocaImageGamePlay.sprite = currentVocabulary.image;
        vocaImageEndLv.sprite = currentVocabulary.image;
        vocaTextGamePlayTest.text = currentVocabulary.vocabulary;
        vocaTextEndLv.text = currentVocabulary.vocabulary;
        vocaMeaningText.text = currentVocabulary.mean;
        currentWordLength = currentVocabulary.vocabulary.Length;
        AudioManager.instance.SetCurrentWordAudio(currentVocabulary.audio);
        PerfectWordHolder.instance.ActiveSlots();
        AlphabetHolder.instance.GetAlphabets();
    }
    private void Update()
    {
        if(timer > 0f && !timeOut && !EndLvUI.gameObject.activeInHierarchy)
        {
            timer -= Time.deltaTime;
        }
        timerText.text = (int)timer + "";
        CheckTimeOut();
    }
    private void CheckTimeOut()
    {
        if(!timeOut && timer <= 0f && !selectDiffUI.gameObject.activeInHierarchy)
        {
            Debug.Log("Time Out!");
            timeOut = true;
            life--;
            blurBlackScreen.gameObject.SetActive(true);
            TimeOutNoti.gameObject.SetActive(true);
            Invoke("DisappearAHeart", 1f);
        }
    }
    private void DisappearAHeart()
    {
        hearts[life].Find("Heart_RedFx").gameObject.SetActive(true);
    }
    public void OnSelectDifficulty(int diff)
    {
        switch(diff)
        {
            case 0:
                DifficultyManager.instance.Mode = Difficulty.easy;
                timer = 10f;
                GetRandomEasyVocabulary();
                break;
            case 1:
                DifficultyManager.instance.Mode = Difficulty.normal;
                timer = 20f;
                GetRandomMediumVocabulary();
                break;
            case 2:
                DifficultyManager.instance.Mode = Difficulty.hard;
                timer = 30f;
                GetRandomHardVocabulary();
                break;
        }
        selectDiffUI.gameObject.SetActive(false);
        AudioManager.instance.PlayCurrentWordAudio();
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
    public void OnClickNextLv()
    {
        PerfectWordHolder.instance.ReturnAllAlphabetToHolder();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                GetRandomEasyVocabulary();
                timer = 10f;
                break;
            case Difficulty.normal:
                GetRandomMediumVocabulary();
                timer = 20f;
                break;
            case Difficulty.hard:
                GetRandomHardVocabulary();
                timer = 30f;
                break;
        }
        if(outOfVoca)
        {
            // enable win UI
            return;
        }
        EndLvUI.gameObject.SetActive(false);
        TimeOutNoti.gameObject.SetActive(false);
        blurBlackScreen.gameObject.SetActive(false);
        currentAlphabetNumOnSlot = 0;
        timeOut = false;
        AudioManager.instance.PlayCurrentWordAudio();
    }
    public void EnableEndGameUI() => EndLvUI.gameObject.SetActive(true);
    public void CheckEndLv()
    {
        if (currentWordLength != currentAlphabetNumOnSlot)
            return;
        if (PerfectWordHolder.instance.CheckPerfectWordWhenFullSlot())
        {
            PassLvEffect();
            Invoke("EnablePassLvUI", 1.5f);
        }
    }
    public void PassLvEffect()
    {
        PerfectWordHolder.instance.CreateFx();
    }
    private void EnablePassLvUI() => EndLvUI.gameObject.SetActive(true);
}
