using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class home : MonoBehaviour
{
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject map;
    private pause _pause;
    private bool statusHome = false;
    private bool statusMap = false;

    void Start()
    {
        option.SetActive(false);
        map.SetActive(false);
        _pause = GetComponent<pause>();
    }

    public void controllHome()
    {
        statusHome = !statusHome;
        if (statusHome == true)
        {
            _pause.Stop();
            option.SetActive(true);
        }
        else {
            _pause.Continue();
            option.SetActive(false);
        
        }

    }
     public void controllMap()
    {
        statusMap = !statusMap;
        if (statusMap == true)
        {
            map.SetActive(true);
        }
        else if (statusMap == false) {
            map.SetActive(false);
        }
    }
}
