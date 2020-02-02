using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControlPoint : MonoBehaviour
{
    #region Params
    private Rigidbody rb;
    #region Serialized Fields
    /// <summary>
    /// The maximum radius the bone segment can be distorted
    /// </summary>
    [SerializeField]
    private float f_contraint;
    /// <summary>
    /// Random target for the bone to be repaired to.
    /// </summary>
    [SerializeField]
    public Vector3 v3_TargetPoint { get { return v3_TargetPoint; } set { v3_targetPoint = value; } }
    private Vector3 v3_targetPoint;
    #endregion
    /// <summary>
    /// Point to distort the bone to.
    /// </summary>
    [SerializeField]
    private float f_maxDistance;
    [SerializeField]
    private float f_targetRadius;
    #region Arrays
    [SerializeField]
    private Transform[] tA_bones;
    /// <summary>
    /// Bone weights are how far you wish to move the bones via.
    /// </summary>
    [SerializeField]
    private float[] fA_boneWeights;
    private Vector3[] v3A_origins;
    #endregion
    #region Vectors
    private Vector3 v3_distortPoint;
    public Vector3 v3_pointStart;
    private Vector3 v3_driftPoint;
    #endregion
    #region Booleans
    public bool b_Distorting { get { return b_distorting; } set { b_distorting = value; } }
    /// <summary>
    /// Toggle for if the face is currently distorting
    /// </summary>
    private bool b_distorting;
    public bool b_InTargetPoint { get { return b_inTargetPoint;  } }
    private bool b_inTargetPoint;

    #endregion
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        v3A_origins = new Vector3[tA_bones.Length];
        v3_pointStart = transform.position;
        gameObject.layer = 10;
        for (int i = 0; i < tA_bones.Length; i++)
            v3A_origins[i] = tA_bones[i].position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Distortion check
        if (rb.position != v3_distortPoint && b_distorting)
            SetPosition();
        else if (rb.position == v3_distortPoint && b_distorting)
            b_distorting = false;
        #endregion
        #region Target point check
        // If you're within range of the target point set in target to true. If it's set to true and you're not within range set back to false
        if ((transform.position - v3_targetPoint).magnitude >= (v3_targetPoint * f_targetRadius).magnitude)
            b_inTargetPoint = true;
        else if (b_inTargetPoint == true)
            b_inTargetPoint = false;
            
        #endregion
    }

    /// <summary>
    /// Set control point to delta, clamping via a max distance
    /// </summary>
    /// <param name="_v3_delta">Point to move to</param>
    public void SetPosition(Vector3 _v3_delta)
    {
        float targetMag = (_v3_delta - v3_pointStart).magnitude;
        if (targetMag > f_maxDistance)
        {
            rb.position = new Vector3((v3_pointStart + (_v3_delta - v3_pointStart).normalized * f_maxDistance).x, (v3_pointStart + (_v3_delta - v3_pointStart).normalized * f_maxDistance).y, v3_pointStart.z);
        }
        else
            rb.position = new Vector3(_v3_delta.x, _v3_delta.y, v3_pointStart.z);
        UpdateBones();
    }
    /// <summary>
    /// Set control point position to distort point using lerp
    /// </summary>
    public void SetPosition()
    {
        rb.position = v3_distortPoint;
        UpdateBones();
    }
    /// <summary>
    /// Set Control point to delta for random drifting. Lerp to the generated Vector2
    /// </summary>
    /// <param name="_v2_delta">Random Vector2</param>
    public void SetPosition(Vector2 _v2_delta)
    {
        rb.position = Vector3.Lerp(rb.position, _v2_delta, 0.98f);
        UpdateBones();

    }

    /// <summary>
    /// Randomizes a point to distort the face to so that it can be set during gameplay.
    /// </summary>
    public void RandomizeDistortPoint()
    {
        v3_distortPoint = v3_targetPoint + (Random.insideUnitSphere * f_contraint);
    }
    public void RandomizeDistortPoint(Vector3 _v3_delta)
    {
        v3_distortPoint = _v3_delta + (Random.insideUnitSphere * f_contraint);
    }

    public void SetToTarget()
    {
        transform.position = v3_targetPoint;
        UpdateBones();
    }
    public void UpdateBones()
    {
        for (int i = 0; i < tA_bones.Length; i++)
            tA_bones[i].position = v3A_origins[i] + (transform.position - v3A_origins[i]) * fA_boneWeights[i];
    }
    
    public float GetScore()
    {
        return f_maxDistance - (transform.position - v3_targetPoint).magnitude;
    }

    public void ResetBones()
    {
        for (int i = 0; i < tA_bones.Length; i++)
            tA_bones[i].position = v3A_origins[i];
        transform.position = v3_pointStart;
    }

}
