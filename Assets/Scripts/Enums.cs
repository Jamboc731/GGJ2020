using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum Character
{
    john,
    bart,
    brett,
    louise,
    nat,
    byron,
    jame,
    wilson
}
public enum GameStates
{
    mainmenu,
    training,
    running,
    paused,
    exit
}