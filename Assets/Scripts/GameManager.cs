using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Enums
    public GameStates gameState;
    public Character levelCharacter;
    #endregion
    #region Level SO Variables
    [SerializeField] private LevelSO[] so_levels;
    [SerializeField] private ControlPoint[] controlPoints;

    // An array that stores the story number that you're on, and the level.
    [SerializeField] StoryToTargets stt_storyToTargets;
    [SerializeField] private GameObject go_background;
    [TextArea] private string[] s_storyTexts;
    #endregion
    [SerializeField] [TextArea] private string s_textToShow;
    [SerializeField] [TextArea] private string s_errorText;
    StoryToTargets[] sttA_targets;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int _i_levelID)
    {
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
            levelCharacter = levelToLoad.character;
            s_storyTexts = levelToLoad.texts;
            sttA_targets = levelToLoad.storyTargets;
            //for (int i = 0; i < levelToLoad.boneOrigins.Length; i++)
            //{
            //    storyToTargets.i_storyID[i] = i;
            //    for (int j = 0; j < storyToTargets.t_targetPoints.Length; j++)
            //        storyToTargets.t_targetPoints[j] = levelToLoad.boneOrigins[i][j];
            //}
            SelectStoryText();
        }
        else
            s_textToShow = s_errorText;

    }

    public void SelectStoryText()
    {
        int storyID = Random.Range(0, s_storyTexts.Length);
        s_textToShow = s_storyTexts[storyID];

        for (int i = 0; i < sttA_targets[storyID].t_targetPoints.Length; i++)
        {
            controlPoints[i].v3_TargetPoint = sttA_targets[storyID].t_targetPoints[i];
            controlPoints[i].SetToTarget();
        }
    }
}
