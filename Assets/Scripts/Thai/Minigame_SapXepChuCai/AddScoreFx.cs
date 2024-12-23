using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddScoreFx : MonoBehaviour
{
    private Animator anim;
    public TextMeshProUGUI addScoreText;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        addScoreText = GetComponent<TextMeshProUGUI>();
    }
    private void OnFinishAnim()
    {
        GameManager_SXChuCai.instance.AddBonusScore();
        gameObject.SetActive(false);
    }
}
