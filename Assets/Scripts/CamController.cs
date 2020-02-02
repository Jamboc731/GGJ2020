using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour
{

    public static CamController x;

    [SerializeField] private Vector3 V3_menuPosition;
    [SerializeField] private Vector3 V3_gamePosition;
    [SerializeField] private Image[] Im_blackImages;
    [SerializeField] private float f_lerpTime;

    private void Awake()
    {
        x = this;
    }

    public void MenuPosition(bool _b_showBlack)
    {
        StartCoroutine(GoToPoint(V3_menuPosition, f_lerpTime));
        foreach (var i in Im_blackImages)
        {
            i.CrossFadeAlpha(_b_showBlack ? 1 : 0, f_lerpTime, true);
        }
    }

    public void GamePosition(bool _b_showBlack)
    {
        StartCoroutine(GoToPoint(V3_gamePosition, f_lerpTime));
        foreach (var i in Im_blackImages)
        {
            i.CrossFadeAlpha(_b_showBlack ? 1 : 0, f_lerpTime, true);
        }

    }

    IEnumerator GoToPoint(Vector3 _V3_pos, float _f_time)
    {
        float f = 0;
        Vector3 startPos = transform.position;
        while(f <= 1)
        {
            transform.position = startPos * (1 - f) + _V3_pos * f;
            f += Time.deltaTime * (1 / _f_time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.position = _V3_pos;
    }

}
