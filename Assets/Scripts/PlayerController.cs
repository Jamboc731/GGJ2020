using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region SerializedVariables

    [SerializeField] private GameObject[] goA_touchPoints;
    [SerializeField] private Camera cam;
    [SerializeField] private float f_playWidth;
    [SerializeField] private float f_playHeight;

    #endregion

    #region PrivateVariables
    private Vector3 V3_screenNormalizationScale;
    private Vector3 V3_playAreaBound;

    private RaycastHit hit;
    private Ray ray;

    #endregion

    private void Start()
    {

        Screen.orientation = ScreenOrientation.Portrait;

        V3_screenNormalizationScale = new Vector3(((float)1 / Screen.width), ((float)1/Screen.height), 0);
        V3_playAreaBound = new Vector3(f_playWidth, f_playHeight, 1);
        
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            ray = cam.ScreenPointToRay(Input.touches[0].position);
            Debug.DrawRay(ray.origin, ray.direction * 15, Color.red);
            if (Physics.Raycast(ray, out hit, 200))
            {
                ControlPoint c = hit.collider.gameObject.GetComponent<ControlPoint>();
                if (c != null) 
                {
                    //Debug.Log(Vector3.Scale(NormaliseTouchInput(Input.touches[0].position), V3_playAreaBound));
                    c.SetPosition(Vector3.Scale(NormaliseTouchInput(Input.touches[0].position), V3_playAreaBound));
                    //c.SetPosition(Vector3.one);
                }
            }
        }
        //if (Input.touchCount > 0) goA_touchPoints[Input.touches[0].fingerId + 1].transform.position = Vector3.Scale(NormaliseTouchInput(Input.touches[0].position), V3_playAreaBound);
        //if (Input.touchCount > 1) goA_touchPoints[Input.touches[1].fingerId].transform.position = Vector3.Scale(NormaliseTouchInput(Input.touches[1].position), V3_playAreaBound);

        //goA_touchPoints[0].transform.position = new Vector3((f_playWidth / 2) + (f_playWidth / 2) * Input.acceleration.x, f_playHeight / 2, 0);


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
