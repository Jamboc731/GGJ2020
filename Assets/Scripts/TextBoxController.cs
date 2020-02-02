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

    public void DisplayText(string newText)
    {
        enabled = true;
        textToShow = newText;
        for (int i = 0; i < textToShow.Length; i++)
        {
            textCurrentlyShown += textToShow[i];
            StartCoroutine(TextWait(i * 0.1f));
            t_thisText.text = textCurrentlyShown;
        }
        StartCoroutine(TextKill(f_waitTime * textToShow.Length));
    }

    void OnDisable()
    {
        t_timer.b_Running = true;
    }

    IEnumerator TextWait(float _f_textTime)
    {
        yield return new WaitForSeconds(f_waitTime + _f_textTime);
    }
    IEnumerator TextKill(float _f_textTime)
    {
        yield return new WaitForSeconds(f_waitTime + _f_textTime);
        enabled = false;
    }
}