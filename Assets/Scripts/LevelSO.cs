﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "")]
public class LevelSO : ScriptableObject
{

    public int ID;
    public GameObject backgroundObject;
    [TextArea] public string[] texts;
    public Vector3[] boneOrigins;

}