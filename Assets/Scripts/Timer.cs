using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float f_MaxTime { get { return f_maxTime; } set { f_MaxTime = value; } }
    private float f_maxTime;
    public float f_CurrentTime { get { return f_currentTime; } set { f_currentTime = value; } }
    private float f_currentTime;
    public bool b_Running { get { return b_running; } set { b_running = value; } }
    private bool b_running;

    Slider s_slider;

    // Start is called before the first frame update
    void Start()
    {
        s_slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        f_currentTime -= b_running ? Time.deltaTime : 0;
        GameManager.x.gameState = f_currentTime < 0 ? GameStates.gameover : GameStates.running;

        if(f_currentTime <= 0)
        {
            TriggerEnd();
        }
        s_slider.value = f_CurrentTime / f_maxTime;

    }

    void TriggerEnd()
    {
        b_Running = false;

        GameManager.x.SelectStoryText();
    }

}
