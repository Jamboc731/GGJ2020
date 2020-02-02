using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    [SerializeField] Timer t_timer;
    [SerializeField] float f_waitTime;
    string textToShow;
    string textCurrentlyShown;
    Text t_thisText;

    private void Start()
    {
    }

    public void DisplayText(string newText)
    {
        t_thisText = GetComponentInChildren<Text>();

        gameObject.SetActive(true);
        textToShow = newText;



        t_thisText.text = newText;

        StartCoroutine(TextKill(f_waitTime));

    }

    void OnDisable()
    {
        t_timer.b_Running = true;
        GameManager.x.DistortFace();
    }

    
    IEnumerator TextKill(float _f_textTime)
    {
        yield return new WaitForSeconds(f_waitTime + _f_textTime);
        gameObject.SetActive(false);
    }
}