using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region SerializedVariables

    //[SerializeField] private GameObject[] goA_touchPoints;
    [SerializeField] private Camera cam;
    [SerializeField] private int i_maxFingers = 2;
    [SerializeField] private LayerMask controlPointMask;

    #endregion

    #region PrivateVariables
    [SerializeField]
    private ControlPoint[] cp_currentControlPoints = new ControlPoint[2];
    private float f_playWidth;
    private float f_playHeight; 
    private Vector3 V3_screenNormalizationScale;
    private Vector3 V3_playAreaBound;
    private RaycastHit hit;
    private Ray ray;

    #endregion

    private void Start()
    {

        Screen.orientation = ScreenOrientation.Portrait;

        cp_currentControlPoints = new ControlPoint[i_maxFingers];
        V3_screenNormalizationScale = new Vector3(((float)1 / Screen.width), ((float)1 / Screen.height), 1);
        V3_playAreaBound = new Vector3(f_playWidth, f_playHeight, 1);

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            //ray = perspCam.ScreenPointToRay(Input.touches[0].position);
            //Debug.DrawRay(ray.origin, ray.direction * 15, Color.blue);


            #region new

            for (int i = 0; i < Input.touchCount && i < i_maxFingers; i++)
            {
                ray = cam.ScreenPointToRay(Input.touches[i].position);
                Debug.DrawRay(ray.origin, ray.direction * 15, Color.red);

                if (Input.touches[i].phase == TouchPhase.Began)
                {
                    //Debug.Log("press");
                    if (Physics.Raycast(ray, out hit, 20, controlPointMask))
                    {
                        //Debug.Log("hit control point");
                        cp_currentControlPoints[Input.touches[i].fingerId] = hit.collider.GetComponent<ControlPoint>();
                        cp_currentControlPoints[Input.touches[i].fingerId].b_Drifting = false;
                    }
                }

                if (cp_currentControlPoints[Input.touches[i].fingerId] != null)
                {
                    cp_currentControlPoints[Input.touches[i].fingerId].SetPosition(Vector3.Scale(NormaliseTouchInput(Input.touches[i].position), V3_playAreaBound));

                    if (Input.touches[i].phase == TouchPhase.Ended)
                    {
                        cp_currentControlPoints[Input.touches[i].fingerId].b_Drifting = true;
                        cp_currentControlPoints[Input.touches[i].fingerId].RandomizedDrifting();
                        cp_currentControlPoints[Input.touches[i].fingerId] = null;

                    }
                }


            }

            #endregion

            #region old
            //if (Physics.Raycast(ray, out hit, 200, controlPointMask))
            //{
            //    ControlPoint c = hit.collider.gameObject.GetComponent<ControlPoint>();
            //    if (c != null) 
            //    {
            //        //Debug.Log(Vector3.Scale(NormaliseTouchInput(Input.touches[0].position), V3_playAreaBound));

            //        c.SetPosition(Vector3.Scale(NormaliseTouchInput(Input.touches[0].position), V3_playAreaBound));
            //        //c.SetPosition(Vector3.one);
            //    }
            //}
            #endregion

        }

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
