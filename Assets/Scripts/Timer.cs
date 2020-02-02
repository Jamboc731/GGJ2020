using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float f_MaxTime { get { return f_maxTime; } set { f_MaxTime = value; } }
    private float f_maxTime;
    private float f_currentTime;
    public bool b_Running { get { return b_running; } set { b_running = value; } }
    private bool b_running;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_currentTime -= b_running ? Time.deltaTime : 0;
        GameManager.x.gameState = f_currentTime < 0 ? GameStates.gameover : GameStates.running;
    }

}
