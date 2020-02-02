using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] GameObject fuckingWork;
    public float f_MaxTime { get { return f_maxTime; } set { f_maxTime = value; } }
    private float f_maxTime;
    public float f_CurrentTime { get { return f_currentTime; } set { f_currentTime = value; } }
    private float f_currentTime;
    public bool b_Running { get { return b_running; } set { b_running = value; } }
    private bool b_running = false;

    int segmentsToComplete = 4;
    int currentSegment = 0;
    Slider s_slider;

    // Start is called before the first frame update
    void Start()
    {
        s_slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("heh" + f_currentTime);
        //Debug.Log(b_Running);

        f_currentTime -= b_running ? Time.deltaTime : 0;
        //Debug.Log(f_currentTime);
        GameManager.x.gameState = f_currentTime < 0 ? GameStates.gameover : GameStates.running;

        if (f_currentTime <= 0)
        {
            TriggerEnd();
        }
        //Debug.Log(f_currentTime);
        s_slider.value = f_CurrentTime / f_maxTime;

    }

    void TriggerEnd()
    {
        if (b_running && currentSegment == segmentsToComplete)
        {
            b_running = false;

            MenuManager.x.ChangeActiveCanvas(fuckingWork);
            MenuManager.x.ResetFaceToNeutral();
            MenuManager.x.DisableGameplayControls();

        }
        else
        {
            GameManager.x.SelectStoryText();
            currentSegment++;
        }
        //GameManager.x.SelectStoryText();
    }

}
