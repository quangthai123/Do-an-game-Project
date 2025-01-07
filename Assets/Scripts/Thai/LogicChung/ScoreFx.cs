using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreFx : MonoBehaviour
{
    private Animator anim;
    public TextMeshProUGUI scoreText;
    [SerializeField] private Minigame minigame = Minigame.SXChuCai;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void OnFinshAnim()
    {
        switch (minigame)
        {
            case Minigame.SXChuCai:
                GameManager_SXChuCai.instance.AddScore();
                break;
            case Minigame.DaoAnh:
                GameManagerDaoAnh.Instance.AddScore();
                break;
        }
        gameObject.SetActive(false);
    }
}
