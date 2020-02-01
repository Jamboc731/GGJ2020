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

    private GameObject background;
    [TextArea] private string[] s_storyTexts;
    #endregion
    [SerializeField] [TextArea] private string s_textToShow;
    [SerializeField] [TextArea] private string s_errorText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool LoadLevel(int _i_levelID)
    {
        // Load in parameters for the level
        LevelSO levelToLoad = null;
        foreach (LevelSO level in so_levels)
            if (level.ID == _i_levelID)
                levelToLoad = level;
        if (levelToLoad != null)
        {
            // Set the level background and all the bones target points
            background = levelToLoad.backgroundObject;
            levelCharacter = levelToLoad.character;
            s_storyTexts = levelToLoad.texts;
            for (int i = 0; i < levelToLoad.boneOrigins.Length; i++)
            {
                for (int j = 0; j < levelToLoad.boneOrigins[i].Length; j++)
                controlPoints[i].v3_TargetPoint = levelToLoad.boneOrigins[i][j];
            }
            return true;
        }
        else
            s_textToShow = s_errorText;
            return false;

    }

    public void SelectStoryText()
    {
        s_textToShow = s_storyTexts[Random.Range(0, s_storyTexts.Length)];
    }
}
