using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDaoChu : GameManager
{
    public static GameManagerDaoChu Instance;
    public Action onPlayerTouchingAction;
    public Action onInitializeLv;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject); 
        else
            Instance = this;
    }
    public void OnTouching() => onPlayerTouchingAction?.Invoke();
    private void InitializeLv() => onInitializeLv?.Invoke();
    public override void OnSelectDifficulty(int diff)
    {
        base.OnSelectDifficulty(diff);
        InitializeLv();
    }
}
