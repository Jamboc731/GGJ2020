﻿using System.Collections;
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
    /// <summary>
    /// Toggle for if the face is currently distorting
    /// </summary>
    private bool b_distorting;
    [SerializeField]
    private Transform[] hb_bones;
    /// <summary>
    /// Bone weights are how far you wish to move the bones via.
    /// </summary>
    [SerializeField]
    private float[] boneWeights;
    private Vector3[] v3_origins;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        v3_origins = new Vector3[hb_bones.Length];
        for (int i = 0; i < hb_bones.Length; i++)
            v3_origins[i] = hb_bones[i].position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Distortion check
        if (rb.position != v3_distortPoint && b_distorting)
            SetPosition(v3_distortPoint);
        else if (rb.position == v3_distortPoint && b_distorting)
            b_distorting = false;
        #endregion
    }

    public void SetPosition(Vector3 _v3_delta)
    {
        rb.MovePosition(Vector3.Lerp(rb.position, _v3_delta, 0.95f));
        for (int i = 0; i < hb_bones.Length; i++)
            hb_bones[i].position = v3_origins[i] + (transform.position - v3_origins[i]) * boneWeights[i];
    }

    /// <summary>
    /// Randomizes a point to distort the face to so that it can be set during gameplay.
    /// </summary>
    public void RandomizeDistortPoint()
    {
        v3_distortPoint = v3_targetPoint + (Random.insideUnitSphere * f_contraint);
    }
    /// <summary>
    /// See if the face is currently distorting.
    /// </summary>
    /// <returns>bool</returns>
    public bool GetDistorting()
    {
        return b_distorting;
    }
    /// <summary>
    /// Manually set if the face is distorting
    /// </summary>
    /// <param name="_b_delta">The change that you are making</param>
    public void SetDistorting(bool _b_delta)
    {
        b_distorting = _b_delta;
    }
    /// <summary>
    /// Switch if the face is currently distorting
    /// </summary>
    public void SetDistorting()
    {
        b_distorting = !b_distorting;
    }
}