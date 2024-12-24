using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDaoChu : GameManager
{
    public static GameManagerDaoChu Instance;
    public Action onPlayerTouchingAction;
    public Action onSetUpLv;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject); 
        else
            Instance = this;
    }
    private void Start()
    {
        SetUpLv();
    }
    public void OnTouching() => onPlayerTouchingAction?.Invoke();
    private void SetUpLv() => onSetUpLv?.Invoke();
}
