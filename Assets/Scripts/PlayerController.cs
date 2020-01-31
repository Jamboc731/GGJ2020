using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region SerializedVariables

    [SerializeField] private GameObject[] goA_touchPoints;
    [SerializeField] private float f_playWidth;
    [SerializeField] private float f_playHeight;

    #endregion

    #region PrivateVariables
    private Vector3 V3_screenNormalizationScale;
    private Vector3 V3_playAreaBound;

    #endregion

    private void Start()
    {
        V3_screenNormalizationScale = new Vector3(((float)1 / Screen.width), ((float)1/Screen.height), 0);
        V3_playAreaBound = new Vector3(f_playWidth, f_playHeight, 1);
    }

    private void Update()
    {
        if (Input.touchCount > 0) goA_touchPoints[Input.touches[0].fingerId].transform.position = Vector3.Scale(NormaliseTouchInput(Input.touches[0].position), V3_playAreaBound);
        if (Input.touchCount > 1) goA_touchPoints[Input.touches[1].fingerId].transform.position = Vector3.Scale(NormaliseTouchInput(Input.touches[1].position), V3_playAreaBound);

        //if(Input.touchCount > 0) Debug.Log("0 - " + Input.touches[0].fingerId);
        //if(Input.touchCount > 1) Debug.Log("1 - " + Input.touches[1].fingerId);

    }

    /// <summary>
    /// This function normalises the position of the touch based on the screen height and width
    /// </summary>
    /// <param name="_V3_touchPoint">This is the point of the touch on the screen</param>
    /// <returns>The noramlised point of the touch</returns>
    private Vector3 NormaliseTouchInput(Vector3 _V3_touchPoint)
    {
        return Vector3.Scale(_V3_touchPoint, V3_screenNormalizationScale);
    }

}
