using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Idle", true);
    }
    public void SetAnim(string _animName)
    {
        foreach(AnimatorControllerParameter para in anim.parameters)
        {
            anim.SetBool(para.name, false);
        }
        anim.SetBool(_animName, true);
    }
    private void OnFinshAnim() => SetAnim("Idle");
}
