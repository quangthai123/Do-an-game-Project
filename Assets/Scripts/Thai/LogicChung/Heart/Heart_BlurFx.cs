using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_BlurFx : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnFinishAnim()
    {
        GameManager_SXChuCai.instance.EnableEndGameUI();
        gameObject.SetActive(false);
    }
}
