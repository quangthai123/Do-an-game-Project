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
    [SerializeField] private Image vocaImageGOVUI;
    [SerializeField] private TextMeshProUGUI vocaTextGamePlayTest;
    [SerializeField] private TextMeshProUGUI vocaTextEndLv;
    [SerializeField] private TextMeshProUGUI vocaTextGOVUI;
    [SerializeField] private TextMeshProUGUI vocaMeaningText;
    [SerializeField] private TextMeshProUGUI vocaMeaningTextGOVUI;
    [SerializeField] private Transform selectDiffUI;
    [SerializeField] private Transform endLvUI;
    [SerializeField] private Transform gameOverUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private List<Transform> hearts;
    [SerializeField] private Transform blurBlackScreen;
    [SerializeField] private Transform timeOutNoti;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform scoreFx;
    [SerializeField] private Transform addScoreFx;
    [SerializeField] private TextMeshProUGUI lvText;
    [SerializeField] private int score = 0;
    [SerializeField] private int addScore = 0;
    [SerializeField] private BackgroundMoving bg;
    [SerializeField] private PlayPartUI playPartUI;
    [SerializeField] private PerfectWordHolder perfectWordHolder;
    private int life = 3;
    private float timer = 0;
    private int lv = 1;
    private bool timeOut = false;
    private bool outOfVoca = false;
    private bool startTimer = false;
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
        endLvUI.gameObject.SetActive(false);
        scoreFx.gameObject.SetActive(false);
        addScoreFx.gameObject.SetActive(false);
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
        vocaImageGOVUI.sprite = currentVocabulary.image;
        vocaTextGamePlayTest.text = currentVocabulary.vocabulary;
        vocaTextEndLv.text = currentVocabulary.vocabulary;
        vocaTextGOVUI.text = currentVocabulary.vocabulary;
        vocaMeaningText.text = currentVocabulary.mean;
        vocaMeaningTextGOVUI.text = currentVocabulary.mean;
        currentWordLength = currentVocabulary.vocabulary.Length;
        AudioManager.instance.SetCurrentWordAudio(currentVocabulary.audio);
        perfectWordHolder.ActiveSlots();
        AlphabetHolder.instance.GetAlphabets();
    }
    private void Update()
    {
        if(timer > 0f && startTimer && !timeOut)
        {
            timer -= Time.deltaTime;
        }
        timerText.text = (int)timer + "";
        scoreText.text = score+"";
        lvText.text = "Level " + lv;
        if(addScoreFx.gameObject.activeInHierarchy)
            addScoreFx.GetComponent<AddScoreFx>().addScoreText.text = "+"+addScore;
        CheckTimeOut();
    }
    private void CheckTimeOut()
    {
        if(!timeOut && timer <= 0f && startTimer)
        {
            Debug.Log("Time Out!");
            timeOut = true;
            startTimer = false;
            life--;
            blurBlackScreen.gameObject.SetActive(true);
            timeOutNoti.gameObject.SetActive(true);
            Invoke("TimeOutFx", 1f);
        }
    }
    private void TimeOutFx()
    {
        if(life > 0)
            Player.Instance.SetAnim("Hit");
        else
            Player.Instance.SetAnim("Dead");
        hearts[life].Find("Heart_RedFx").gameObject.SetActive(true);
    }
    public void OnSelectDifficulty(int diff)
    {
        switch(diff)
        {
            case 0:
                DifficultyManager.instance.Mode = Difficulty.easy;
                timer = 16f;
                GetRandomEasyVocabulary();
                break;
            case 1:
                DifficultyManager.instance.Mode = Difficulty.normal;
                timer = 26f;
                GetRandomMediumVocabulary();
                break;
            case 2:
                DifficultyManager.instance.Mode = Difficulty.hard;
                timer = 41f;
                GetRandomHardVocabulary();
                break;
        }
        selectDiffUI.gameObject.SetActive(false);
        SetRunToNextLvState();
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
        perfectWordHolder.ReturnAllAlphabetToHolder();
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                GetRandomEasyVocabulary();
                timer = 16f;
                break;
            case Difficulty.normal:
                GetRandomMediumVocabulary();
                timer = 26f;
                break;
            case Difficulty.hard:
                GetRandomHardVocabulary();
                timer = 41f;
                break;
        }
        if(outOfVoca)
        {
            // enable win UI
            return;
        }
        endLvUI.gameObject.SetActive(false);
        timeOutNoti.gameObject.SetActive(false);
        blurBlackScreen.gameObject.SetActive(false);
        currentAlphabetNumOnSlot = 0;
        addScore = 0;
        if (!timeOut)
        {
            lv++;
            SetRunToNextLvState();
        }    
        else
        {
            SetPlayState();
            timeOut = false;
        }
    }
    public void OnSelectExitBtn()
    {
        endLvUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        selectDiffUI.gameObject.SetActive(true);
        ResetGameState();
    }
    public void EnableEndGameUI()
    {
        if (life != 0)
            endLvUI.gameObject.SetActive(true);
        else
            gameOverUI.gameObject.SetActive(true);
    }
    public void CheckEndLv()
    {
        if (currentWordLength != currentAlphabetNumOnSlot)
            return;
        if (perfectWordHolder.CheckPerfectWordWhenFullSlot())
        {
            startTimer = false;
            PassLvEffect();
            scoreFx.gameObject.SetActive(true);
            switch (DifficultyManager.instance.Mode)
            {
                case Difficulty.easy:
                    scoreFx.GetComponent<ScoreFx>().scoreText.text = "+10";
                    break;
                case Difficulty.normal:
                    scoreFx.GetComponent<ScoreFx>().scoreText.text = "+20";
                    break;
                case Difficulty.hard:
                    scoreFx.GetComponent<ScoreFx>().scoreText.text = "+30";
                    break;
            }
            Invoke("EnablePassLvUI", 3.5f);
        }
    }
    public void PassLvEffect()
    {
        perfectWordHolder.CreateFx();
        Player.Instance.SetAnim("Victory");
    }
    private void EnablePassLvUI()
    {
        endLvUI.gameObject.SetActive(true);
        playPartUI.SetOnState(false);
        PlayWordAudio();
    }
    public void AddScore()
    {
        switch (DifficultyManager.instance.Mode)
        {
            case Difficulty.easy:
                score += 10;
                break;
            case Difficulty.normal:
                score += 20;
                break;
            case Difficulty.hard:
                score += 30;
                break;
        }
        addScoreFx.gameObject.SetActive(true);
        StartCoroutine(AddScoreByTimerRemain());
    }
    private IEnumerator AddScoreByTimerRemain()
    {
        int intTimer = (int)timer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(.05f);
            timer -= 1;
            intTimer -= 1;
            if(intTimer >= 0)
                addScore += 1;
            if (timer < 0)
                timer = 0;
        }
    }
    private void ResetGameState()
    {
        life = 3;
        score = 0;
        lv = 1;
        currentAlphabetNumOnSlot = 0;
        addScore = 0;
        timeOut = false;
        perfectWordHolder.ReturnAllAlphabetToHolder();
        timeOutNoti.gameObject.SetActive(false);
        blurBlackScreen.gameObject.SetActive(false);
        playPartUI.SetOnState(false);
        for(int i=0; i < 3; i++ )
        {
            hearts[i].GetComponent<Image>().color = Color.white;
            hearts[i].Find("Heart_RedFx").gameObject.SetActive(false);
            hearts[i].Find("Heart_BlurFx").gameObject.SetActive(false);
        }
    }
    public void AddBonusScore() => score += addScore;
    private void SetRunToNextLvState()
    {
        startTimer = false;
        playPartUI.SetOnState(false);
        Player.Instance.SetAnim("Run");
        bg.MoveBG();
        Invoke("SetPlayState", 2f);
    }
    private void SetPlayState()
    {
        startTimer = true;
        playPartUI.SetOnState(true);
        Player.Instance.SetAnim("Idle");
        bg.StopMoveBG();
        Invoke("PlayWordAudio", .5f);
    }
    private void PlayWordAudio() => AudioManager.instance.PlayCurrentWordAudio();
}
