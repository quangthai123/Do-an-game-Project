using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDaoChu : GameManager
{
    public static GameManagerDaoChu Instance;
    public Action onPlayerTouchingAction;
    public Action onInitializeLv;
    private bool invokeIntializeLv = false;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject); 
        else
            Instance = this;
    }
    private void Update()
    {
        if (!invokeIntializeLv)
        {
            InitializeLv();
            invokeIntializeLv = true;
        }
    }
    public void OnTouching() => onPlayerTouchingAction?.Invoke();
    private void InitializeLv() => onInitializeLv?.Invoke();
}
