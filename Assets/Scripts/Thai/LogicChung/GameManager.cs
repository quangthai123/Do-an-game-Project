using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    protected int lv = 1;
    [SerializeField] protected int score = 0;
    [Header("UI")]
    [SerializeField] protected Transform selectDiffUI;
    [SerializeField] protected GameObject pauseUI;
    [SerializeField] protected Transform blurBlackScreen;
    public Vocabulary currentVocabulary;
    protected int life = 3;
    protected float timer = 0;
    protected bool timeOut = false;
    protected bool outOfVoca = false;
    protected bool startTimer = false;
    protected virtual void Start()
    {
        selectDiffUI.gameObject.SetActive(true);
        pauseUI.SetActive(false);
    }
    public virtual void OnSelectDifficulty(int diff)
    {
        switch (diff)
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
        selectDiffUI.gameObject.SetActive(false);
    }
}
