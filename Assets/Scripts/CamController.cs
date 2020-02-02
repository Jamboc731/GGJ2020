using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour
{

    [SerializeField] private Vector3 V3_menuPosition;
    [SerializeField] private Vector3 V3_gamePosition;
    [SerializeField] private Image Im_blackImage;

    public void MenuPosition(float _f_lerpTime, bool _b_showBlack)
    {
        StartCoroutine(GoToPoint(V3_menuPosition, _f_lerpTime));
        Im_blackImage.CrossFadeAlpha(_b_showBlack ? 1 : 0, _f_lerpTime, true);
    }

    public void GamePosition(float _f_lerpTime, bool _b_showBlack)
    {
        StartCoroutine(GoToPoint(V3_gamePosition, _f_lerpTime));
        Im_blackImage.CrossFadeAlpha(_b_showBlack ? 1 : 0, _f_lerpTime, true);

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
