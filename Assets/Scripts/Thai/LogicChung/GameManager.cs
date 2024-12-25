using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour
{
    protected int lv = 1;
    [SerializeField] protected int score = 0;
    public Vocabulary currentVocabulary;
    protected int life = 3;
    protected float timer = 0;
    protected bool timeOut = false;
    protected bool outOfVoca = false;
    protected bool startTimer = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
