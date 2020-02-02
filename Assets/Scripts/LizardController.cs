using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardController : MonoBehaviour
{

    private Animator a;

    private bool b_animating;
    public bool b_Animating { get { return b_animating; } set { b_animating = value; } }

    private void Start()
    {
        a = GetComponent<Animator>();
    }

    public void SetAnimationBool(string _s_param, bool _b_state)
    {
        a.SetBool(_s_param, _b_state);
    }

    public void BackToIdle()
    {
        a.SetBool("Fusetouching", false);
        a.SetBool("Goodjob", false);
    }
}
