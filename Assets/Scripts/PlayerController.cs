using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public static PlayerController x;

    #region SerializedVariables

    //[SerializeField] private GameObject[] goA_touchPoints;
    [SerializeField] private Camera cam;
    [SerializeField] private LizardController lizzy;
    [SerializeField] private int i_maxFingers = 2;
    [SerializeField] private LayerMask controlPointMask;
    [SerializeField] private LayerMask menuMask;
    [SerializeField] private AudioClip touchClip;
    [SerializeField] private Text scoreText;

    #endregion

    #region PrivateVariables
    private SpriteRenderer[] cp_allControlRenderers;
    private ControlPoint[] cp_allControlPoints;
    private ControlPoint[] cp_currentControlPoints = new ControlPoint[2];
    private Vector3 V3_screenNormalizationScale;
    private Vector3 V3_facePos = new Vector3(0, 0, 22);
    private Vector3 V3_zFlatten = new Vector3(1, 1, 0);
    private RaycastHit hit;
    private Ray ray;
    private Color targetColor;
    private bool b_canControl = true;
    private bool b_playing = true;
    private bool b_won = false;
    private float f_score;
    #endregion

    #region GetsAndSets
    public bool b_Won { get { return b_won; } }
    public bool b_CanControl { get { return b_canControl; } set { b_canControl = value; } }
    public float f_Score { get { return f_score; } }
    #endregion

    private void Awake()
    {
        x = this;
    }

    private void Start()
    {
        cp_allControlPoints = GameManager.x.cp_ControlPoints;
        cp_allControlRenderers = new SpriteRenderer[cp_allControlPoints.Length];
        for (int i = 0; i < cp_allControlPoints.Length; i++)
        {
            cp_allControlRenderers[i] = cp_allControlPoints[i].GetComponent<SpriteRenderer>();
        }

        Screen.orientation = ScreenOrientation.Portrait;

        cp_currentControlPoints = new ControlPoint[i_maxFingers];
        V3_screenNormalizationScale = new Vector3(((float)1 / Screen.width), ((float)1 / Screen.height), 1);

    }
    private void Update()
    {

        if (b_canControl)
        {
            RecieveTouches();
            FadePoints();
            if (b_playing) CheckWinState();
        }
        else TouchUI();
    }

    private void SetScoreText(float _f_score)
    {
        scoreText.text = _f_score.ToString();
    }

    private void TouchUI()
    {
        if (Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                ray = cam.ScreenPointToRay(Input.touches[0].position);
                //Debug.DrawRay(ray.origin, ray.direction.normalized * 10, Color.blue);
                if (Physics.Raycast(ray, out hit, -cam.transform.position.z + 10, menuMask))
                {
                    //print("touching ui");
                    IPressable p = hit.collider.GetComponent<IPressable>();
                    if (p != null) p.press();
                }

            }
        }
    }

    private void RecieveTouches()
    {
        if (Input.touchCount > 0)
        {
            targetColor = Color.white;
            #region new

            for (int i = 0; i < Input.touchCount && i < i_maxFingers; i++)
            {
                ray = cam.ScreenPointToRay(Input.touches[i].position);
                //Debug.DrawRay(ray.origin, ray.direction.normalized * 10, Color.blue);

                if (Input.touches[i].phase == TouchPhase.Began)
                {

                    IPressable p = null;

                    Physics.Raycast(ray, out hit, -cam.transform.position.z + 15, menuMask);
                    if (hit.collider != null)
                    {
                        p = hit.collider.GetComponent<IPressable>();
                        Debug.Log("col");
                        if (p != null)
                        {
                            Debug.Log("p");
                            p.press();
                        }

                    }
                    Physics.Raycast(ray, out hit, -cam.transform.position.z + 10, controlPointMask);
                    if (hit.collider != null)
                    {

                        if (!lizzy.b_Animating) lizzy.SetAnimationBool("Fusetouching", true);
                        cp_currentControlPoints[Input.touches[i].fingerId] = hit.collider.GetComponent<ControlPoint>();
                    }
                }

                if (cp_currentControlPoints[Input.touches[i].fingerId] != null)
                {
                    cp_currentControlPoints[Input.touches[i].fingerId].SetPosition(cam.ScreenToWorldPoint(Vector3.Scale(Input.touches[i].position, V3_zFlatten) + V3_facePos));

                    if (Input.touches[i].phase == TouchPhase.Ended)
                    {
                        cp_currentControlPoints[Input.touches[i].fingerId] = null;
                        AudioManager.x.PlaySFX(touchClip, .8f, 1.2f);

                    }
                }

            }

            #endregion

        }
        else
        {
            if (b_won) lizzy.SetAnimationBool("Goodjob", true);
            else
                lizzy.BackToIdle();
            targetColor = Color.clear;
        }
    }

    private void CheckWinState()
    {
        int targ = cp_allControlPoints.Length;
        int cur = 0;
        f_score = 0;
        foreach (var c in cp_allControlPoints)
        {
            if (c.b_InTargetPoint) cur++;
            f_score += c.GetScore();
        }
        b_won = cur == targ;
        b_playing = !b_won;

    }

    private void FadePoints()
    {
        foreach (var p in cp_allControlRenderers) p.color = Color.Lerp(p.color, targetColor, 0.9f);
    }


}
