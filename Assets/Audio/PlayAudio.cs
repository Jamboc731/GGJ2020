using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioManager AM;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AM.PlayVoice(0, 5, 15);
            print("pressed space");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
}
