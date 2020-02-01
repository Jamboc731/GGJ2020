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
    #endregion

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
            background = levelToLoad.backgroundObject;
            levelCharacter = levelToLoad.character;
            for (int i = 0; i < levelToLoad.boneOrigins.Length; i++)
            {
                controlPoints[i].v3_TargetPoint = levelToLoad.boneOrigins[i];
            }

        }

    }
}
