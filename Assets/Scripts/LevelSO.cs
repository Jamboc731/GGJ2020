using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StoryToTargets
{
    [SerializeField] public Vector3[] t_targetPoints;
}

[CreateAssetMenu(fileName = "", menuName = "New Level")]
public class LevelSO : ScriptableObject
{
    public int ID;
    public GameObject backgroundObject;
    [TextArea] public string[] texts;
    public StoryToTargets[] storyTargets;
    public Character[] character;
    public float[] f_segmentTimes;
}