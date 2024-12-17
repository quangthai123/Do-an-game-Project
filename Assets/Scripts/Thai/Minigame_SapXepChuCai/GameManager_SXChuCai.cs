using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_SXChuCai : GameManager
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
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private List<GameObject> perfectWordHolders;
    [SerializeField] private TextMeshProUGUI titleEndLvText;
    [SerializeField] private GameObject exitOrReplayNoti;
    [SerializeField] private TextMeshProUGUI exitOrReplayTitleTxt;
    [SerializeField] private TextMeshProUGUI scoreNotiOnExitOrReplayNoti;
    [SerializeField] private int addScore = 0;
    [SerializeField] private BackgroundMoving bg;
    [SerializeField] private PlayPartUI playPartUI;
    private int life = 3;
    private float timer = 0;
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
        exitOrReplayNoti.SetActive(false);
        pauseUI.SetActive(false);
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
        for(int i=0; i<=3; i++)
        {
            perfectWordHolders[i].SetActive(false);
        }
        if (DifficultyManager.instance.Mode != Difficulty.hard)
        {
            perfectWordHolders[0].SetActive(true);
            perfectWordHolders[0].GetComponent<PerfectWordHolder>().ActiveSlots();
        }
        else
        {
            for(int i=1; i<=GetCurrentVocaPartQuantity(); i++)
            {
                perfectWordHolders[i].SetActive(true);
                perfectWordHolders[i].GetComponent<PerfectWordHolder>().ActiveSlotsOnHardMode();
            }
        }
        AlphabetHolder.instance.GetAlphabets();
    }
    public int GetVocaPartLength(int partIndex)
    {
        return GetCurrentVocaPartsOnHardMode()[partIndex-1].Length;
    }
    private string[] GetCurrentVocaPartsOnHardMode()
    {
        string[] vocaParts = currentVocabulary.vocabulary.Split(new string[] { " " }, System.StringSplitOptions.None);
        return vocaParts;
    }
    public int GetCurrentVocaPartQuantity()
    {   
        Debug.Log(GetCurrentVocaPartsOnHardMode().Length);
        return GetCurrentVocaPartsOnHardMode().Length;
    }
    private void Update()
    {
        if(timer > 0f && startTimer && !timeOut)
        {
            timer -= Time.deltaTime;
        }
        timerText.text = (int)timer + "";
        scoreText.text = score+"";
        scoreNotiOnExitOrReplayNoti.text = score + "";
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
            titleEndLvText.text = "Ouch!";
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
        VocabularyManager.instance.ResetVocabulariesRemain();
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
    public bool CheckAlphabet(char alphabet, int index, int holderLocation)
    {
        if (holderLocation != 0)
        {
            return this.GetCurrentVocaPartsOnHardMode()[holderLocation-1][index] == alphabet;
        }
        string voca = currentVocabulary.vocabulary;
        return voca[index] == alphabet;
    }
    public void OnClickNextLv()
    {
        for(int i=0; i<=3; i++)
        {
            perfectWordHolders[i].GetComponent<PerfectWordHolder>().ReturnAllAlphabetToHolder();
        }
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
        exitOrReplayTitleTxt.text = "Bạn muốn chơi lại?";
        exitOrReplayNoti.SetActive(true);
    }
    public void OnSelectQuitGameBtn()
    {
        exitOrReplayTitleTxt.text = "Bạn muốn thoát game?";
        exitOrReplayNoti.SetActive(true);
    }
    public void OnClickOkExitOrReplayBtn()
    {
        endLvUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        selectDiffUI.gameObject.SetActive(true);
        ResetGameState();
    }
    public void OnClickNoExitOrReplayBtn()
    {
        exitOrReplayNoti.SetActive(false);
    }
    public void EnableEndGameUI()
    {
        if (life != 0)
            endLvUI.gameObject.SetActive(true);
        else
            gameOverUI.gameObject.SetActive(true);
    }
    private bool CheckPerfectWordWhenFullSlot()
    {
        if(DifficultyManager.instance.Mode != Difficulty.hard) 
            return perfectWordHolders[0].GetComponent<PerfectWordHolder>().CheckPerfectWordWhenFullSlot();
        else
        {
            for(int i=1;i<=GetCurrentVocaPartQuantity(); i++)
            {
                if (!perfectWordHolders[i].GetComponent<PerfectWordHolder>().CheckPerfectWordWhenFullSlot())
                {
                    Debug.Log("hàng "+i+" sai!");        
                    return false;
                }
            }
            return true;
        }
    }
    public void CheckEndLv()
    {
        if (currentWordLength != currentAlphabetNumOnSlot && DifficultyManager.instance.Mode != Difficulty.hard)
            return;
        if (this.CheckPerfectWordWhenFullSlot())
        {
            startTimer = false;
            PassLvEffect();
            scoreFx.gameObject.SetActive(true);
            titleEndLvText.text = "Wonderful!";
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
        for (int i = 0; i <= 3; i++)
        {
            perfectWordHolders[i].GetComponent<PerfectWordHolder>().CreateFx();
        }
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
        startTimer = false;
        for (int i = 0; i <= 3; i++)
        {
            perfectWordHolders[i].GetComponent<PerfectWordHolder>().ReturnAllAlphabetToHolder();
        }
        timeOutNoti.gameObject.SetActive(false);
        blurBlackScreen.gameObject.SetActive(false);
        playPartUI.SetOnState(false);
        pauseUI.SetActive(false);
        exitOrReplayNoti.SetActive(false);
        Time.timeScale = 1;
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
    public void OnClickPauseGame()
    {
        if(!startTimer)
            return;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }
    public void OnClickContinueGame()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }
}
