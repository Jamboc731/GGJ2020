using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager x;

    #region Enums
    public GameStates gameState;
    public Character ch_currentCharacter;
    private Character[] ch_levelCharacter;
    #endregion
    #region Level SO Variables
    [SerializeField] private LevelSO[] so_levels;
    [SerializeField] private ControlPoint[] cp_controlPoints;
    public ControlPoint[] cp_ControlPoints { get { return cp_controlPoints; } }

    // An array that stores the story number that you're on, and the level.
    [SerializeField] StoryToTargets stt_storyToTargets;
    [SerializeField] private GameObject go_background;
    [TextArea] private List<string> s_storyTexts;
    #endregion
    [SerializeField] [TextArea] private string s_errorText;
    [SerializeField] Timer timer;
    StoryToTargets[] sttA_targets;

    [SerializeField] TextBoxController tb_textBox;
    private float[] segmentTimes;

    int maxRecursions = 4;

    // Start is called before the first frame update
    void Start()
    {
        ResetFace();
        CamController.x.MenuPosition(false);
    }

    void Awake()
    {
        x = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(int _i_levelID)
    {
        gameState = GameStates.loading;
        // Load in parameters for the level
        LevelSO levelToLoad = null;
        foreach (LevelSO level in so_levels)
            if (level.ID == _i_levelID)
                levelToLoad = level;
        if (levelToLoad != null)
        {
            Destroy(go_background);
            go_background = null;
            // Set the level background and all the bones target points
            go_background = levelToLoad.backgroundObject;
            Instantiate(go_background);
            s_storyTexts = levelToLoad.texts;
            sttA_targets = levelToLoad.storyTargets;
            segmentTimes = levelToLoad.f_segmentTimes;
            //for (int i = 0; i < levelToLoad.boneOrigins.Length; i++)
            //{
            //    storyToTargets.i_storyID[i] = i;
            //    for (int j = 0; j < storyToTargets.t_targetPoints.Length; j++)
            //        storyToTargets.t_targetPoints[j] = levelToLoad.boneOrigins[i][j];
            //}
            SelectStoryText();
        }
    }

    public void SelectStoryText()
    {
        int storyID = Random.Range(0, s_storyTexts.Count);
        ch_currentCharacter = ch_levelCharacter[storyID];
        timer.f_MaxTime = segmentTimes[storyID];
        for (int i = 0; i < sttA_targets[storyID].t_targetPoints.Length; i++)
        {
            cp_controlPoints[i].v3_TargetPoint = sttA_targets[storyID].t_targetPoints[i];
            cp_controlPoints[i].SetToTarget();
        }
        PlayAudio.playAudio.PlayVoice(ch_currentCharacter);
        timer.b_Running = true;
        tb_textBox.DisplayText(s_storyTexts[storyID]);
        s_storyTexts.RemoveAt(storyID);
        gameState = GameStates.running;
    }

    public void DistortFace()
    {
        StartCoroutine(FUCKHIMUP());
    }

    IEnumerator FUCKHIMUP()
    {
        for (int i = 0; i < maxRecursions; i++)
        {
            for (int j = 0; j < cp_controlPoints.Length; j++)
                cp_controlPoints[j].RandomizeDistortPoint(cp_controlPoints[j].v3_pointStart);
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
    }

    public void ResetFace()
    {

        for (int i = 0; i < cp_controlPoints.Length; i++)
        {
            //Debug.Log(i);
            cp_controlPoints[i].transform.position = cp_controlPoints[i].v3_pointStart;
            cp_controlPoints[i].ResetBones();
            cp_controlPoints[i].UpdateBones();
        }
    }
}
