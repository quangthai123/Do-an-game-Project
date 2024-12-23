using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDaoChu : GameManager
{
    public static GameManagerDaoChu Instance;
    public Action onPlayerTouchingAction;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject); 
        else
            Instance = this;
    }
    public void OnTouching()
    {
        onPlayerTouchingAction?.Invoke();
    }
}
