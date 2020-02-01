using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControlPoint : MonoBehaviour
{
    #region Params
    private Rigidbody rb;
    /// <summary>
    /// The maximum radius the bone segment can be distorted
    /// </summary>
    [SerializeField]
    private float f_contraint;
    /// <summary>
    /// Random target for the bone to be repaired to.
    /// </summary>
    [SerializeField]
    private Vector3 v3_targetPoint;
    /// <summary>
    /// Point to distort the bone to.
    /// </summary>
    private Vector3 v3_distortPoint;
    public bool b_Distorting { get { return b_distorting; } set { b_distorting = value; } }
    /// <summary>
    /// Toggle for if the face is currently distorting
    /// </summary>
    private bool b_distorting;
    [SerializeField]
    private Transform[] t_bones;
    /// <summary>
    /// Bone weights are how far you wish to move the bones via.
    /// </summary>
    [SerializeField]
    private float[] boneWeights;
    [SerializeField]
    private float f_maxDistance;

    private bool drifitng;
    private Vector3 v3_pointStart;
    private Vector3[] v3_origins;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        v3_origins = new Vector3[t_bones.Length];
        v3_pointStart = transform.position;
        gameObject.layer = 9;
        for (int i = 0; i < t_bones.Length; i++)
            v3_origins[i] = t_bones[i].position;
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
    }

    /// <summary>
    /// Set control point to delta, clamping via a max distance
    /// </summary>
    /// <param name="_v3_delta">Point to move to</param>
    public void SetPosition(Vector3 _v3_delta)
    {

        float targetMag = (_v3_delta - v3_pointStart).magnitude;
        if (targetMag > f_maxDistance)
            rb.position = v3_pointStart + (_v3_delta - v3_pointStart).normalized * f_maxDistance;
        else
            rb.position = _v3_delta;
        for (int i = 0; i < t_bones.Length; i++)
            t_bones[i].position = v3_origins[i] + (transform.position - v3_origins[i]) * boneWeights[i];
    }
    /// <summary>
    /// Set control point position to distort point using lerp
    /// </summary>
    public void SetPosition()
    {
        rb.position = Vector3.Lerp(rb.position, v3_distortPoint, 0.98f);
        for (int i = 0; i < t_bones.Length; i++)
            t_bones[i].position = v3_origins[i] + (transform.position - v3_origins[i]) * boneWeights[i];
    }

    /// <summary>
    /// Randomizes a point to distort the face to so that it can be set during gameplay.
    /// </summary>
    public void RandomizeDistortPoint()
    {
        v3_distortPoint = v3_targetPoint + (Random.insideUnitSphere * f_contraint);
    }

}
