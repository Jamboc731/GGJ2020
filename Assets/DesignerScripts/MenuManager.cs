using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private GameObject go_currentActiveCanvas;
    [Space]
    [SerializeField] private GameObject go_ScenarioGamePlayCanvas;
    [SerializeField] private GameObject go_FreestyleGamePlayCanvas;

    //[Header("Other Managers")]
    private AudioManager am_audioManager;

    private void Start()
    {
        am_audioManager = AudioManager.x;
    }
    public void ChangeActiveCanvas(GameObject _go_targetCanvas)
    {
        go_currentActiveCanvas.SetActive(false);
        go_currentActiveCanvas = _go_targetCanvas;

        go_currentActiveCanvas.SetActive(true);
    }
       

    #region Scenario Selections
    public void SelectedRestaurantDay(int _i_level)
    {
        // gameManager.LoadLevel(_i_level)
        print("Should've turned the restaurant(day) on");
    }
    public void SelectedRestaurantNight()
    {
        print("Should've turned the restaurant(night) on");
    }
    public void SelectedWedding()
    {
        print("Should've turned the wedding on");
    }
    #endregion

    #region Difficulty Selections
    public void ChosenEasyDifficulty()
    {
        print("Chosen easy");
    }
    public void ChosenMediumDifficulty()
    {
        print("Chosen medium");
    }
    public void ChosenHardDifficulty()
    {
        print("Chosen hard");
    }
    #endregion

    #region Face Controls
    public void ActivateGameplayControl()
    {
        print("Should've turned the controls on, but I don't know how");
    }
    public void DisableGameplayControls()
    {
        print("Face has been turned off");
    }

    public void ResetFaceToNeutral()
    {
        print("Should've reset the face to neutral, but I don't know how");
    }

    public void ResumeFace()
    {
        print("Face has been resumed");
    }

    public void PauseFace()
    {
        print("Should've paused the face, but I don't know how");
    }
    #endregion

    #region Settings Controls

    #region Colour Controls
    public void SetToProtanomaly()
    {
        print("Set to Protanomaly colour");
    }
    public void SetToDeuteronomaly()
    {
        print("Set to Deuteronomaly colour");
    }
    public void SetToTritonomlay()
    {
        print("Set to Tritonomaly colour");
    }
    #endregion

    #region Audio Controls
    public void ChangeMusicVolume()
    {
        am_audioManager.ToggleMusic();
    }

    public void ChangeFXVolume()
    {
        am_audioManager.ToggleSFX();
    }

    #endregion

    #endregion


}
