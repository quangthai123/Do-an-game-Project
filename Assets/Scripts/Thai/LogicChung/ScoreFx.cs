using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreFx : MonoBehaviour
{
    private Animator anim;
    public TextMeshProUGUI scoreText;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void OnFinshAnim()
    {
        GameManager_SXChuCai.instance.AddScore();
        gameObject.SetActive(false);
    }
}
